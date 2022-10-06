using MyApp.Data.DTOs;
using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsers();
        public Task<User?> GetUserById(int userId);
        public Task AddUser(User user);
        public Task UpdateUser(User user);
        public Task<User?> DeleteUser(int userId);
        public Task<bool> IsEmailAvailable(string email);
    }
}
