using MyApp.Data.Models;

namespace MyApp.Data.Repositories
{
    public interface IRoleRepository
    {
        public Task<List<Role>> GetAllRoles();
        public Task<Role?> GetRoleById(int roleId);
    }
}
