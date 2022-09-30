using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NewAPI.Model;
using NewAPI.Security;
using NewAPI.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskManagementAPI.DTOs;
using TaskManagementAPI.Repository.Interface;
using TaskManagementAPI.Services.Interface;

namespace TaskManagementAPI.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepo;
        private readonly IMapper _mapper;
        private readonly NewAPI.Security.ITokenService _jwtSecurity;

        public AccountService(IAccountRepository accountRepo, IMapper mapper, NewAPI.Security.ITokenService jwtSecurity)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
            _jwtSecurity = jwtSecurity;
        }
        //check if entity is null, check if email could be found, check if passoword in the dto's entry == user model's own, map,claims, if all has been met, return a jwt response
        public async Task<AuthenticatedResponse> LoginUser(UserLoginDto loginDto)
        {
            if (loginDto == null)
                return new AuthenticatedResponse
                {
                    Token = "Invalid user login Dto",
                    RefreshToken = "Invalid user login Dto"
                };
            var userEmail = await _accountRepo.EmailFound(loginDto.Email);
            if (!await _accountRepo.ComfirmPassword(userEmail, loginDto.Password))
                return new AuthenticatedResponse
                {
                    Token = "User not authorized!",
                    RefreshToken = "User not authorized!"
                };

            var users = _mapper.Map<User>(loginDto);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, users.Email),
                //new Claim(ClaimTypes.Name, users.UserName)
            };

            var roles = await _accountRepo.GetUserRole(users);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return new AuthenticatedResponse
            {
                Token = await _jwtSecurity.JWTGen(claims),
                RefreshToken = _jwtSecurity.GenerateRefreshToken()
            };
        }
    }
}