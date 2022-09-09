using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NewAPI.DTOs;
using NewAPI.Model;
using NewAPI.Services;
using System.Threading.Tasks;

namespace NewAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddUser(UserToAddDto model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Invalid model");
                return BadRequest(model);
            }
            //check if user email already exists
            if (await _userService.AlreadyExists(model.Email))
            {
                ModelState.AddModelError("", "Email already exists");
                return BadRequest(model);
            }
            //map dto to user model
            var user = _mapper.Map<User>(model); //automapper helps us to do this instead of doing the one commented below

            /*var user = new User
            {
                UserName = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                Email = model.Email,
            };*/

            //create user
            var result = await _userService.AddUser(user, model.Password);
            if(result.Status == false)
            {
                foreach(var err in result.Error.Errors)
                {
                    ModelState.AddModelError(err.Code, err.Description);
                }
                return BadRequest(model);
            }
            return Ok(result.Message);
        }
    }
}
