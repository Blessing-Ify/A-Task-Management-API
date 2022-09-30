using AutoMapper;
using Microsoft.AspNetCore.Identity;
using NewAPI.Data;
using NewAPI.DTOs;
using NewAPI.Model;
using System.Linq;
using System.Threading.Tasks;
using TaskManagementAPI.Repository.Interface;

namespace TaskManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userMgr;
        private readonly APIContext _context;

        public UserRepository(UserManager<User> userMgr, APIContext context)
        {
            _userMgr = userMgr;
            _context = context;
        }

        public async Task<IdentityResult> RegisterUser(User user)
        {
            return await _userMgr.CreateAsync(user);
        }
        public IQueryable<User> GetAllUser()
        {
            throw new System.NotImplementedException();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
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
