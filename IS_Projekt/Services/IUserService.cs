using IS_Projekt.Models;

namespace IS_Projekt.Services
{
    public interface IUserService
    {
        Task<User?> CreateUser(User userData);
        string GenerateToken(User user);
        Task<User?> GetUserByUsername(string username);
        Task<IEnumerable<User>> GetUsers();
        bool VerifyPassword(User user, string providedPassword);
    }
}