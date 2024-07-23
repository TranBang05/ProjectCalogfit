using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DataAcess.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string? Name { get; set; }
        public string? Quantity { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
    }
}
