using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu.DataAccess.Models
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public int MealId { get; set; }
        public Meal? Meal { get; set; }
        public int RecipeId { get; set; }
        
    }
}
