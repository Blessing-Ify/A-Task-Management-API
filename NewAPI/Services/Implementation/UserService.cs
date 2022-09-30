using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewAPI.DTOs;
using NewAPI.Model;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repository.Interface;

namespace NewAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Task<User> GetUser (string id)
        {
            return _userRepository.GetUserById(id);

        }

        //see what you did: public async Task<ServiceReturnType<IdentityResult>> CreateUser (User user)
        public async Task<ServiceReturnType<IdentityResult>> CreateUser(UserRegisterDto userDto)
        {
            //Firstly, validate that the entity is not null
            if (userDto == null)
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false, 
                    Message = "Object must not be null", 
                    Data = null, 
                    Error = null
                };
            //Secondly, check if user email already exists
            if (await _userRepository.AlreadyExists(userDto.Email))
                return new ServiceReturnType<IdentityResult>
                { Status = false, 
                  Message = "Email already exists",
                  Data = null,
                  Error = null
                };
            //Thirdly, if email doesn't exist, then assign user role(you need user model for the AddRoleAsyn hence we map first before proceeding)
            var user = _mapper.Map<User>(userDto);

            var roleResult = await _userRepository.AddRoleAsync(user, userDto.Role);
            if (!roleResult.Succeeded)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false,
                    Message = "User not assigned a role",
                    Data = null,
                    Error = null
                };
            }

            var result = await _userRepository.RegisterUser(user);
            return new ServiceReturnType<IdentityResult>
            {
                Status = true,
                Message = "Added successfully",
                Data = result,
                Error = null
            };
        }

        
    }
}
