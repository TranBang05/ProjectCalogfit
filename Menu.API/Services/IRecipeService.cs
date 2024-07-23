using Menu.API.Dto.Request;
using Menu.API.Dto.Response;

namespace Menu.API.Services
{
    public interface IRecipeService
    {
        Task<IEnumerable<RecipeResponse>> GetRecipesByCategory(RecipeCategoryRequest categoryRequest);
        Task<IEnumerable<RecipeResponse>> GetRecipesByCategorys(string categoryName);
    }
}
