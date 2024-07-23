using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Recipe.API.Dto.Request;
using Recipe.API.Service;
using Microsoft.IdentityModel.JsonWebTokens;


namespace Recipe.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService recipeService)
        {
            _reviewService = recipeService;
        }
        [HttpPost("{recipeId}")]
        //[Authorize] // Bỏ comment để yêu cầu xác thực người dùng
        public async Task<IActionResult> AddReviewAsync(int recipeId, [FromBody] ReviewRequest request)
        {
            
            var userIdClaim = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            if (userIdClaim == 0)
            {
                return Unauthorized("Bạn cần đăng nhập trước khi đánh giá món ăn!");
            }
            if (recipeId != request.RecipeId)
            {
                return BadRequest("RecipeId in the URL and body must match.");
            }

            var reviewRequest = new ReviewRequest
            {
                RecipeId = request.RecipeId,
                Rating = request.Rating,
                Comment = request.Comment
            };

            var reviewResponse = await _reviewService.AddReview(userIdClaim, reviewRequest);
            return CreatedAtAction(nameof(GetReviewsByRecipeId), new { recipeId = reviewResponse.RecipeId }, reviewResponse);
        }

        [HttpGet("{recipeId}/reviews")]
        public async Task<IActionResult> GetReviewsByRecipeId(int recipeId)
        {
            var reviews = await _reviewService.GetReviewsById(recipeId);
            return Ok(reviews);
        }
    }
}
