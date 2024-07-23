using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipe.DataAcess.Models;

namespace Recipe.DataAcess.Repository
{
    public interface IRecipeRepository
    {
        Task<IEnumerable<Models.Recipe>> GetAllRecipes();
        Task<Models.Recipe> GetRecipeById(int recipeId);
        Task<Models.Recipe> CreateRecipe(Models.Recipe recipe);
        Task<Models.Recipe> UpdateRecipe(Models.Recipe recipe);
        Task<bool> DeleteRecipe(int recipeId);
        Task<IEnumerable<Models.Recipe>> GetRecipesByCategory(RecipeCategory category);

        Task<IEnumerable<Step>> GetAllStep();
        Task<IEnumerable<Ingredient>> GetAllIngredient();
    }
}
