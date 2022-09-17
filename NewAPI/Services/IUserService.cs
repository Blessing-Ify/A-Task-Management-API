using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Threading.Tasks;

namespace NewAPI.Services
{
    public interface IUserService
    {
        public Task<ServiceReturnType<IdentityResult>> CreateUser(User user, string password);
        public Task<User> GetUser (string id);
        public Task<bool> AlreadyExists(string email);
        public Task<IdentityResult> AddRoleAsync (User user, string roleName);
    }
}
