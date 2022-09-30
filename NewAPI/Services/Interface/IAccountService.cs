using Microsoft.AspNetCore.Identity;
using NewAPI.Services;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace TaskManagementAPI.Services.Interface
{
    public interface IAccountService
    {
        Task<AuthenticatedResponse> LoginUser(UserLoginDto loginDto);
    }
}
