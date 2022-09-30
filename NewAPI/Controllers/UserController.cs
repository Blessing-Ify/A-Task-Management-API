using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NewAPI.DTOs;
using NewAPI.Services;
using System;
using System.Threading.Tasks;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {        
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody]UserRegisterDto userDtoModel)
        {
             _logger.LogInformation($"Registration attempt for new user");
            try
            {
                var model = await _userService.CreateUser(userDtoModel);
                return Created(userDtoModel.Email, model);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(RegisterUser)}");
                return Problem($"Something Went wrong in the {nameof(RegisterUser)}");
            }
        }         
    }
}