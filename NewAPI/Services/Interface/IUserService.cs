using Microsoft.AspNetCore.Identity;
using NewAPI.DTOs;
using NewAPI.Model;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace NewAPI.Services
{
    public interface IUserService
    {
        //You cannot create a register function and not use the Register Dto you created for it.
        public Task<ServiceReturnType<IdentityResult>> CreateUser(UserRegisterDto user);
        public Task<User> GetUser (string id);
        
    }
}
