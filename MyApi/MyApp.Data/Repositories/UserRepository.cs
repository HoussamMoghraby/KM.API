using Microsoft.EntityFrameworkCore;
using MyApp.Data.Contexts;
using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserContext _userContext;
        public UserRepository(UserContext userContext)
        {
            _userContext = userContext;
        }
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _userContext.Users
                //.AsNoTracking()
                .Include(u => u.Country)
                .Include(u => u.Role)
                .ToListAsync();

            return users;
        }

        public async Task<User?> GetUserById(int userId)
        {
            var user = await _userContext.Users
                .Include(u => u.Country)
                .Include(u => u.Role)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task AddUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _userContext.Users.Update(user);
            await _userContext.SaveChangesAsync();
        }

        public async Task<User?> DeleteUser(int userId)
        {
            var userToDelete = await GetUserById(userId);

            if (userToDelete != null)
            {
                userToDelete.IsActive = false;
                await _userContext.SaveChangesAsync();
            }

            return userToDelete;
        }

        public async Task<bool> IsEmailAvailable(string email)
        {
            var user = await _userContext.Users
                .Where(u => u.Email == email)
                .FirstOrDefaultAsync(); //TODO: change to singleOrDefaultAsync?

            return user == null;

        }
    }
}
