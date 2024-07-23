using Microsoft.AspNetCore.Mvc;
using Recipe.API.Dto.Response;
using Recipe.API.Service;

namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngrendentController : Controller
    {
        private readonly IRecipeService _recipeService;

        public IngrendentController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientResponse>>> GetStep()
        {
            var ingrendent = await _recipeService.GetIngredientResponses();
            return Ok(ingrendent);
        }
    }
}
