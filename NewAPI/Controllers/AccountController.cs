using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accoutService;
        private readonly ILogger<UserController> _logger;
        
        public AccountController(IAccountService accountService, ILogger<UserController> logger)
        {
            _accoutService = accountService;            
            _logger = logger; 
        }

        [HttpPost("loginUser")]
        public async Task<ActionResult> Login([FromBody] UserLoginDto userLogin)
        {
            _logger.LogInformation($"Login attempt for {userLogin.Email}");
            try
            {
                var model = await _accoutService.LoginUser(userLogin);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem($"Something Went wrong in the {nameof(Login)}");
            }
        }
    }
}
