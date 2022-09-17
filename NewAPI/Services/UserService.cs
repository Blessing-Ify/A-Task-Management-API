using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Threading.Tasks;

namespace NewAPI.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userMgr;
        private readonly SignInManager<User> _signInManager;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userMgr = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUser (string id)
        {
            return await _userMgr.FindByEmailAsync(id);
        }

        public async Task<ServiceReturnType<IdentityResult>> CreateUser (User user, string password)
        {
            //validate the entity is not a null object
            if(user == null)
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false, Message = "Object must not be null", Data = null, Error = null
                };
            var result = await _userMgr.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false,
                    Message = "Failed to create",
                    Data = null,
                    Error = result
                };
            }
            return new ServiceReturnType<IdentityResult>
            {
                Status = true,
                Message = "Added successfully",
                Data = result,
                Error = null
            };
        }

        public async Task<bool> AlreadyExists(string email)
        {
            var res = await _userMgr.FindByEmailAsync(email);
            if (res == null)
                return false;
            return true;
        }

        public async Task<IdentityResult> AddRoleAsync(User user, string roleName)
        {
            return await _userMgr.AddToRoleAsync(user, roleName);
        }
    }
}
