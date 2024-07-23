using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.DataAcess.Models;

namespace User.DataAcess.Repositories
{
    public interface IUserRepository
    {
        Task<Models.User> GetUserById(int id);
        Task<IEnumerable<Models.User>> GetAllUsers();
        Task<Models.User> CreateUser(Models.User user);
        Task<Models.User> UpdateUser(Models.User user);
        Task DeleteUser(int id);

        Task AddRoleToUser(int userId, int roleId);
        Task AddPermissionToRole(int roleId, int permissionId);

        Task DeleteRolesFromUser(int userId);
        Task DeletePermissionsFromRoles(int userId);
        Task<IEnumerable<Role>> getAllRoles();
        Task<IEnumerable<Permision>> getAllPermision();
    }
    
    
}
