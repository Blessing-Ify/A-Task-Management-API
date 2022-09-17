using NewAPI.Model;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace NewAPI.Security
{
    public interface IJWTSecurity
    {
        public Task<string> JWTGen(UserLoginDto user);

    }
}
