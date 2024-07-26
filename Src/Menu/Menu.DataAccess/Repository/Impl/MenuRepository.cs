using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Menu.DataAccess.Models;

namespace Menu.DataAccess.Repository.Impl
{
    public class MenuRepository : IMenuRepository
    {
        private readonly MenuDbContext _context;

        public MenuRepository(MenuDbContext context)
        {
            _context = context;
        }

        public void Add(Models.Menu menu)
        {
            _context.Menus.Add(menu);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
