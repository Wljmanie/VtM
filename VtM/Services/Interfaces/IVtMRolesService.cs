using VtM.Models;

namespace VtM.Services.Interfaces
{
    public interface IVtMRolesService
    {
        public Task<bool> IsUserInRoleAsync(VtMUser user, string roleName);
        public Task<IEnumerable<string>> GetUserRolesAsync(VtMUser user);
        public Task<bool> AddUserToRoleAsync(VtMUser user, string roleName);
        public Task<bool> RemoveUserFromRoleAsync(VtMUser user, string roleName);
        public Task<bool> RemoveUserFromRolesAsync(VtMUser user, string[] roles);
        public Task<List<VtMUser>> GetUsersInRoleAsync(string roleName);
        public Task<List<VtMUser>> GetUsersNotInRoleAsync(string roleName);
        public Task<string> GetRoleNameByIdAsync(string roleId);
    }
}
