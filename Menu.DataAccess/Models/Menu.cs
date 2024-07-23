using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.DataAccess.Models
{
    public class Menu
    {
        public int MenuId { get; set; }
        public string? Name {  get; set; }
        public int UserId {  get; set; }
        public DateTime Date { get; set; }
        public MenuType? MenuTypes { get; set; }
        public ICollection<Meal>? Meals { get; set; }
    }
}
