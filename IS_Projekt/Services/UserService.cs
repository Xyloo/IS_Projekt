using IS_Projekt.Exceptions;
using IS_Projekt.Extensions;
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
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly string _jwtKey;

        public UserService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
            _jwtKey = "hardone123hardone123hardone123";
        }


        public async Task<User?> CreateUser(User userData)
        {
            var userExists = await _userRepository.GetUserByUsername(userData.Username);
            var userExistsEmail = await _userRepository.GetUserByEmail(userData.Email);
            if (userExists != null) //if user exists, we can't create a new one
                throw new UsernameExistsException();
            if (userExistsEmail != null)
                throw new EmailExistsException();

            var user = new User();
            user.Username = userData.Username;
            user.Password = _passwordHasher.HashPassword(user, userData.Password);
            user.Email = userData.Email;
            user.Role = userData.Role;
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
            string[] roles = user.Role.Split(",");  //roles in string - separated by ", "
            var claims = new List<Claim>();
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Trim()));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _userRepository.GetUserByUsername(username);
        }
    }
}
