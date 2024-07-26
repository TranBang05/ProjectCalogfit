using Microsoft.AspNetCore.Mvc;
using Recipe.API.Dto.Response;
using Recipe.API.Service;

namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepController : Controller
    {
        private readonly IRecipeService _recipeService;

        public StepController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepResponse>>> GetStep()
        {
            var step = await _recipeService.GetStepResponses();
            return Ok(step);
        }

    }
}
