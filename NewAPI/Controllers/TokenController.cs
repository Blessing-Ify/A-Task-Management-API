using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewAPI.Data;
using NewAPI.Security;
using System;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly APIContext _context;
        public TokenController(ITokenService tokenService, APIContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost]
        [Route("refresh")]
        //public async Task<ActionResult<AuthenticatedResponse>> Refresh([FromQuery]AuthenticatedResponse token, [FromBody] UserLoginDto userLogin)
        //public async Task<ActionResult<AuthenticatedResponse>> Refresh(TokenApiModel tokenApiModel, [FromBody] UserLoginDto userLogin)
        public async Task<ActionResult> Refresh(TokenApiModel tokenApiModel)

        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name; //this is mapped to the Name claim by default
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid client request");
            var newAccessToken = await _tokenService.JWTGen(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            _context.SaveChanges();
            return Ok(new AuthenticatedResponse()
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            });
        }

        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var user = _context.Users.SingleOrDefault(u => u.UserName == username);
            if (user == null) return BadRequest();
            user.RefreshToken = null;
            _context.SaveChanges();
            return NoContent();
        }
    }
}
