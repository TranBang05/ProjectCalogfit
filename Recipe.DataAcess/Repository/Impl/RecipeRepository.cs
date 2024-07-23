using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipe.DataAcess.Models;

namespace Recipe.DataAcess.Repository.Impl
{
    public class RecipeRepository:IRecipeRepository
    {
        private readonly RecipeDbContext _context;

        public RecipeRepository(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.Recipe>> GetAllRecipes()
        {
            return await _context.Recipes
                .Include(r => r.Reviews)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .ToListAsync();
        }

        public async Task<Models.Recipe> GetRecipeById(int recipeId)
        {
            return await _context.Recipes
                .Include(r => r.Reviews)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .FirstOrDefaultAsync(r => r.RecipeId == recipeId);
        }

        public async Task<Models.Recipe> CreateRecipe(Models.Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Models.Recipe> UpdateRecipe(Models.Recipe recipe)
        {
            _context.Entry(recipe).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<bool> DeleteRecipe(int recipeId)
        {
            var recipe = await _context.Recipes.FindAsync(recipeId);
            if (recipe == null)
            {
                return false;
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Models.Recipe>> GetRecipesByCategory(RecipeCategory category)
        {
            if (category == null)
            {
                return await _context.Recipes.ToListAsync();
            }

            return await _context.Recipes.Include(r => r.Reviews)
                .Include(r => r.Ingredients)
                .Include(r => r.Steps)
                .Where(r => r.Category == category)
                .ToListAsync();
        }

        public async Task<IEnumerable<Step>> GetAllStep()
        {

            return await _context.Steps
                .ToListAsync();
        }

        public async Task<IEnumerable<Ingredient>> GetAllIngredient()
        {
            return await _context.Ingredients
                 .ToListAsync();
        }
    }
}
