using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewAPI.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace NewAPI.Security
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userMgr;

        public TokenService(IConfiguration config, UserManager<User> userMgr)
        {
            _config = config;
            _userMgr = userMgr;
        }

        //this is to customize your token
        public async Task<string> JWTGen(IEnumerable<Claim> claims)
        {
            var JWTstring = _config.GetSection("JWT");
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTstring["Key"]));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);
            var getTime = JWTstring.GetValue<int>("ExpiryTime");
                                   
            var tokenOptions = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(getTime),
                signingCredentials: creds

              );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token ;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var JWTstring = _config.GetSection("JWT");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false, //you might want to validate the audience and issuer depending on your use case
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTstring["Key"])),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }



            public async Task<bool> ValidateUser(UserLoginDto userDto)
        {
            var user = await _userMgr.FindByEmailAsync(userDto.Email);
            return (user != null && await _userMgr.CheckPasswordAsync(user, userDto.Password));
        }

    }  

}
