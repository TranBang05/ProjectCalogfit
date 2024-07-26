using Microsoft.AspNetCore.Mvc.Rendering;

namespace Calofit_MVC.Models
{
    public class RecipeResponse
    {
        public int RecipeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<SelectListItem>? Ingredients { get; set; }
        public List<SelectListItem>? Steps { get; set; }
    }
}
