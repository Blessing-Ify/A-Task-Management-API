using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Repository.Interface
{
    public interface IAccountRepository
    {
        Task<bool> ComfirmPassword(User user, string password);
        Task<User> EmailFound(string email);
        Task<IList<string>> GetUserRole(User user);
    }
}
