using Microsoft.EntityFrameworkCore;
using MyApp.Data.Contexts;
using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private UserContext _userContext;
        public RoleRepository(UserContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<List<Role>> GetAllRoles()
        {
            var roles = await _userContext.Roles.ToListAsync();
            return roles;
        }

        public async Task<Role?> GetRoleById(int roleId)
        {
            var role = await _userContext.Roles.SingleOrDefaultAsync(x => x.Id == roleId);
            return role;
        }
    }
}
