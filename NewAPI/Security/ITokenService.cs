using NewAPI.Model;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace NewAPI.Security
{
    public interface ITokenService
    {
        Task<string> JWTGen(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
