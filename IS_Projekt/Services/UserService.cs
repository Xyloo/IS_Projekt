﻿using IS_Projekt.Extensions;
using IS_Projekt.Models;
using IS_Projekt.Repos;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IS_Projekt.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private PasswordHasher<User> _passwordHasher;
        private readonly string _jwtKey;

        public UserService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _jwtKey = jwtSettings.Value.Key;
        }


        public async Task<User> CreateUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = _passwordHasher.HashPassword(user, password);
            user.Role = "user";
            await _userRepository.CreateUser(user);
            return user;
        }

        //get all users
        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetUsers();
        }


        public bool VerifyPassword(User user, string providedPassword)
        {
            var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, providedPassword);

            if (verificationResult == PasswordVerificationResult.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                //new Claim(ClaimTypes.Role, user.Role.Name)
                //add claims for roles
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }
}