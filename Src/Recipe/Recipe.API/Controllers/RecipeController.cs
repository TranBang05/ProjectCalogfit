using Microsoft.AspNetCore.Mvc;
using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;
using Recipe.API.Service;
using Recipe.DataAcess.Models;

namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeResponse>>> GetRecipes()
        {
            var recipes = await _recipeService.GetAllRecipes();
            return Ok(recipes);
        }

        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] RecipeRequest request)
        {
            var response = await _recipeService.AddRecipeAsync(request);
            return CreatedAtAction(nameof(GetRecipes), new { id = response.RecipeId }, response);
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> GetRecipeById(int recipeId)
        {
            try
            {
                var recipeResponse = await _recipeService.GetRecipeById(recipeId);
                return Ok(recipeResponse);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound("khong tim duoc");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
            }
        }
        [HttpGet("category")]
        public async Task<IActionResult> GetAllRecipes([FromQuery] RecipeCategoryRequest? category)
        {
            var recipes = await _recipeService.GetRecipesByCategory(category);
            return Ok(recipes);
        }


    }
}
