using IS_Projekt.Models;

namespace IS_Projekt.Services
{
    public interface IUserService
    {
        Task<User?> CreateUser(string username, string password);
        string GenerateToken(User user);
        Task<IEnumerable<User>> GetUsers();
        bool VerifyPassword(User user, string providedPassword);
    }
}