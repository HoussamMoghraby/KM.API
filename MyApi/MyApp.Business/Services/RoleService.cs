using MyApp.Data.Models;
using MyApp.Data.Repositories;

namespace MyApp.Business.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public async Task<Role?> GetRoleById(int roleId)
        {
            var role = await _roleRepository.GetRoleById(roleId);
            return role;
        }

        public async Task<List<Role>> GetRoles()
        {
            var roles = await _roleRepository.GetAllRoles();
            return roles;
        }
    }
}
