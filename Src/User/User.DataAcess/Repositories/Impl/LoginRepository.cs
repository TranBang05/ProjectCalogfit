using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.DataAcess.Models;

namespace User.DataAcess.Repositories.Impl
{
    public class LoginRepository : ILoginRepository
    {
        private readonly UserDbContext _context;

        public LoginRepository(UserDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<string>> GetPermissionsAsync(int roleId)
        {
            return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.Permissions.PermissionName)
            .ToListAsync();
        }

        public async Task<Models.User> GetUserAsync(string email, string password)
        {
            return await _context.User.Include(s=>s.UserRoles).ThenInclude(sr => sr.Role)
                         .SingleOrDefaultAsync(s => s.Email == email && s.PasswordHash == password);
        }

    }
}
