using Microsoft.AspNetCore.Identity;
using NewAPI.DTOs;
using NewAPI.Model;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementAPI.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IdentityResult> RegisterUser(User user);
        Task<User> GetUserById(string id);
        IQueryable<User> GetAllUser();
        Task<bool> AlreadyExists(string email);
        Task<IdentityResult> AddRoleAsync(User user, string roleName);
    }
}
