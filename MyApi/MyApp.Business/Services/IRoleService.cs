using MyApp.Data.Models;

namespace MyApp.Business.Services
{
    public interface IRoleService
    {
        public Task<List<Role>> GetRoles();
        public Task<Role?> GetRoleById(int roleId);
    }
}
