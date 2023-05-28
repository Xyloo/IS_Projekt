using IS_Projekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IS_Projekt.Services
{
    public class UserService
    {
        private PasswordHasher<User> _passwordHasher;
        private string _jwtKey;

        public UserService(string jwtKey)
        {
            _passwordHasher = new PasswordHasher<User>();
            _jwtKey = jwtKey;
        }


        public User CreateUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = _passwordHasher.HashPassword(user, password);

            // Save the user to the database

            return user;
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
