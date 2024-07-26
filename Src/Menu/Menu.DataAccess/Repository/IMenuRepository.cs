using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.DataAccess.Repository
{
    public interface IMenuRepository
    {
        void Add(Models.Menu menu);
        Task SaveChangesAsync();
    }
}
