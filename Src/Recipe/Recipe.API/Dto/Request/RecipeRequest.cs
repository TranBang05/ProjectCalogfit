using Recipe.DataAcess.Models;

namespace Recipe.API.Dto.Request
{
    public class RecipeRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        //public RecipeCategoryRequest? Category { get; set; }
        public List<IngredientRequest>? Ingredients { get; set; }
        public List<StepRequest>? Steps { get; set; }
        
    }
}
