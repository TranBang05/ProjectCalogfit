using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;
using Recipe.DataAcess.Models;

namespace Recipe.API.Service
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeResponse>> GetAllRecipes();
        Task<IEnumerable<StepResponse>> GetStepResponses();
        Task<IEnumerable<IngredientResponse>> GetIngredientResponses();
        Task<RecipeResponse> GetRecipeById(int recipeId);
        Task<RecipeResponse> AddRecipeAsync(RecipeRequest request);
        Task<IEnumerable<RecipeResponse>> GetRecipesByCategory(RecipeCategoryRequest categoryRequest);
    }
}
