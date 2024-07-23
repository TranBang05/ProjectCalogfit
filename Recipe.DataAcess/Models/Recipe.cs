using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DataAcess.Models
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public RecipeCategory Category { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Ingredient>? Ingredients { get; set; }
        public ICollection<Step>? Steps { get; set; }
        
    }
}
