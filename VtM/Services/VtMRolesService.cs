using Microsoft.AspNetCore.Identity;
using VtM.Data;
using VtM.Models;
using VtM.Services.Interfaces;

namespace VtM.Services
{
    public class VtMRolesService : IVtMRolesService
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<VtMUser> _userManager;

        public VtMRolesService(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<VtMUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        public async Task<bool> AddUserToRoleAsync(VtMUser user, string roleName)
        {
            bool result = (await _userManager.AddToRoleAsync(user, roleName)).Succeeded;
            return result;
        }

        public async Task<string> GetRoleNameByIdAsync(string roleId)
        {
            IdentityRole? role = _context.Roles.Find(roleId);
            if (role == null) return "";
            string result = await _roleManager.GetRoleNameAsync(role);
            return result;
        }

        public async Task<IEnumerable<string>> GetUserRolesAsync(VtMUser user)
        {
            IEnumerable<string> result = await _userManager.GetRolesAsync(user);
            return result;
        }

        public async Task<List<VtMUser>> GetUsersInRoleAsync(string roleName)
        {
            List<VtMUser> result = (await _userManager.GetUsersInRoleAsync(roleName)).ToList();
            return result;
        }

        public async Task<List<VtMUser>> GetUsersNotInRoleAsync(string roleName)
        {
            List<string> userIds = (await _userManager.GetUsersInRoleAsync(roleName)).Select(u => u.Id).ToList();
            List<VtMUser> roleUsers = _context.Users.Where(u => !userIds.Contains(u.Id)).ToList();
            return roleUsers;
        }

        public async Task<bool> IsUserInRoleAsync(VtMUser user, string roleName)
        {
            bool result = await _userManager.IsInRoleAsync(user, roleName);
            return result;
        }

        public async Task<bool> RemoveUserFromRoleAsync(VtMUser user, string roleName)
        {
            bool result = (await _userManager.RemoveFromRoleAsync(user, roleName)).Succeeded;
            return result;
        }

        public async Task<bool> RemoveUserFromRolesAsync(VtMUser user, string[] roles)
        {
            bool result = (await _userManager.RemoveFromRolesAsync(user,roles)).Succeeded;
            return result;
        }
    }
}
