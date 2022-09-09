using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Threading.Tasks;

namespace NewAPI.Services
{
    public interface IUserService
    {
        public Task<ServiceReturnType<IdentityResult>> AddUser(User user, string password);
        public User GetUser { get; }
        public Task<bool> AlreadyExists(string email);
    }
}
