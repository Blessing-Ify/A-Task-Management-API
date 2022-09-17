using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewAPI.DTOs;
using NewAPI.Model;
using NewAPI.Security;
using NewAPI.Services;
using System;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userMgr;
        //private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IJWTSecurity _jwtSecurity;
        private readonly IMapper _mapper;


        public UserController(IUserService userService,
            IJWTSecurity jwtSecurity, 
            IMapper mapper, 
            UserManager<User> userManager, 
            ILogger<UserController> logger)
        {
            _userService = userService; 
            _jwtSecurity = jwtSecurity;
            _userMgr = userManager;
            _mapper = mapper;
           // _logger = logger;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(UserDto userDtoModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid user Dto");
                return BadRequest(ModelState);
            }
            //else
            //check if user email already exists
            if (await _userService.AlreadyExists(userDtoModel.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return BadRequest(ModelState);
            }
            //else
            //map dto to user model
            var user = _mapper.Map<User>(userDtoModel);

            //creating the user
            var result = await _userService.CreateUser(user, userDtoModel.Password);
            if (result.Status == false)
            {
                foreach (var err in result.Error.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(ModelState);
            }
            //add user role
            var roleResult = await _userService.AddRoleAsync(user, userDtoModel.Role);
            if (!roleResult.Succeeded)
            {
                foreach (var err in roleResult.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok($"Added! => Id: " + user.Id);
        }

  
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(UserLoginDto usersdata)
        {
            var token = _jwtSecurity.JWTGen(usersdata);

            if (token.Result == null)
            {
                return Unauthorized();
            }

            return Ok(token.Result);
        }

        [Authorize]
        [HttpPost("loginUser")]
        public async Task<ActionResult<TokenDto>> Login([FromBody] UserLoginDto userLogin)
        {
            //_logger.LogInformation($"Login attempt for {userLogin.Email}");
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid user login Dto");
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _userMgr.FindByEmailAsync(userLogin.Email);
                if (user == null || !await _userMgr.CheckPasswordAsync(user, userLogin.Password))
                    return Unauthorized();

                return new TokenDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Token = await _jwtSecurity.JWTGen(userLogin),
                    FirstName = user.FirstName
                };

            }
            catch (Exception e)
            {
                //_logger.LogError(e, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something Went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }
    }
}