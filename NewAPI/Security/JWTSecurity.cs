using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NewAPI.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;

namespace NewAPI.Security
{
    public class JWTSecurity : IJWTSecurity
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userMgr;

        public JWTSecurity(IConfiguration config, UserManager<User> userMgr)
        {
            _config = config;
            _userMgr = userMgr;
        }

        //this is to customize your token
        public async Task<string> JWTGen(UserLoginDto userlogin)
        {
            if (await ValidateUser(userlogin) == false)
                return null;

            var user = await _userMgr.FindByEmailAsync(userlogin.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userMgr.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var JWTstring = _config.GetSection("JWT");
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTstring["Key"]));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha512);

            var tokenOptions = new JwtSecurityToken(

                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds

              );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return token;
        }

        public async Task<bool> ValidateUser(UserLoginDto userDto)
        {
            var user = await _userMgr.FindByEmailAsync(userDto.Email);
            return (user != null && await _userMgr.CheckPasswordAsync(user, userDto.Password));
        }
    }  

}
