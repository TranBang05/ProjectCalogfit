using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;

namespace Recipe.API.Service
{
    public interface IReviewService
    {
        Task<ReviewResponse> AddReview(int userId, ReviewRequest request);
        Task<IEnumerable<ReviewResponse>> GetReviews();
        Task<IEnumerable<ReviewResponse>> GetReviewsById(int recipeId);

    }
}
