using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repository.Interface;

namespace TaskManagementAPI.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<User> _userMgr;
        private readonly IMapper _mapper;
        public AccountRepository(UserManager<User> userMgr, IMapper mapper)
        {
            _mapper = mapper;
            _userMgr = userMgr;
        }
        public async Task<bool> ComfirmPassword(User user, string password)
        {
            return await _userMgr.CheckPasswordAsync(user, password);
               
        } 
        public async Task<User> EmailFound(string email)
        {
            return await _userMgr.FindByEmailAsync(email);            
        }
        public async Task<IList<string>> GetUserRole(User user)
        {
            return await _userMgr.GetRolesAsync(user);               
        }
    }
}
