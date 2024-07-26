using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.DataAcess.Models;

namespace User.DataAcess.Repositories
{
    public interface ILoginRepository
    {
        Task<User.DataAcess.Models.User> GetUserAsync(string email, string password);
        Task<IEnumerable<string>> GetPermissionsAsync(int roleId);
    }
}
