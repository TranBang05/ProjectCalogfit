using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.DataAcess.Models;

namespace User.DataAcess.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.User>> GetAllUsers()
        {
            return await _context.User
                .Include(u => u.UserRoles)
                    .ThenInclude(sr => sr.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permissions)
                .ToListAsync();
        }

        public async Task<Models.User> CreateUser(Models.User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }  

        public async Task<Models.User> GetUserById(int id)
        {
            return await _context.User
                .Include(u => u.UserRoles)
                    .ThenInclude(sr => sr.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permissions)
                .SingleOrDefaultAsync(u => u.UserId== id);
        }

        public async Task<Models.User> UpdateUser(Models.User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task AddRoleToUser(int userId, int roleId)
        {
            var uRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };
            _context.UserRoles.Add(uRole);
            await _context.SaveChangesAsync();
        }

        public async Task AddPermissionToRole(int roleId, int permissionId)
        {
            var rolePermission = new RolePermission
            {
                RoleId = roleId,
                PermissionId = permissionId
            };
            _context.RolePermissions.Add(rolePermission);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRolesFromUser(int userId)
        {
            var rolesToRemove = _context.UserRoles.Where(sr => sr.UserId == userId);
            _context.UserRoles.RemoveRange(rolesToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermissionsFromRoles(int userId)
        {
            var roles = _context.UserRoles.Where(sr => sr.UserId == userId).Select(sr => sr.RoleId).Distinct();
            var permissionsToRemove = _context.RolePermissions.Where(rp => roles.Contains(rp.RoleId));
            _context.RolePermissions.RemoveRange(permissionsToRemove);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Role>> getAllRoles()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task<IEnumerable<Permision>> getAllPermision()
        {
            return await _context.Permissions.ToListAsync();
        }
    }
}
