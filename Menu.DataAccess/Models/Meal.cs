using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.DataAccess.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public MealType? MealTypes { get; set; }
        public int MenuId { get; set; }
        public Menu? Menu { get; set; }
        public ICollection<MenuItem>? MenuItems { get; set; }
    }
}
