using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Threading.Tasks;

namespace NewAPI.Services
{
    public class UserService : IUserService
    {
        private UserManager<User> _userMgr;

        public User GetUser { get ; private set; }

        public UserService(UserManager<User> userManager)
        {
            _userMgr = userManager;
        }
        public async Task<ServiceReturnType<IdentityResult>> AddUser(User user, string password)
        {
            //validate the entity is not a null object
            if(user == null)
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false, Message = "Object must not be null", Data = null, Error = null
                };
            var res = await _userMgr.CreateAsync(user, password);
            if (!res.Succeeded)
            {
                return new ServiceReturnType<IdentityResult>
                {
                    Status = false,
                    Message = "Failed to create",
                    Data = null,
                    Error = res
                };

            }
            return new ServiceReturnType<IdentityResult>
            {
                Status = true,
                Message = "Added successfully",
                Data = res,
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
    }

    public class ServiceReturnType<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public T Error  { get; set; }
    }
}
