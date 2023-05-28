using IS_Projekt.Models;

namespace IS_Projekt.Repos
{
    public interface IUserRepository
    {
        Task<User> CreateUser(User user);
        Task DeleteUser(int id);
        Task<User> GetUser(int id);
        Task<IEnumerable<User>> GetUsers();
        Task<User> UpdateUser(User user);
    }
}