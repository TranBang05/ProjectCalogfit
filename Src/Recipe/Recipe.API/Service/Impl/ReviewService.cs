using AutoMapper;
using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;
using Recipe.DataAcess.Models;
using Recipe.DataAcess.Repository;

namespace Recipe.API.Service.Impl
{
    public class ReviewService:IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ReviewResponse> AddReview(int userId, ReviewRequest request)
        {
            var review = _mapper.Map<Review>(request);
            review.UserId = userId; 
            review.CreatedDate = DateTime.UtcNow;
            var addedReview = await _repository.AddReviewAsync(review);
            return _mapper.Map<ReviewResponse>(addedReview);
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviews()
        {
            var reviews = await _repository.GetReviews();
            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }

        public async Task<IEnumerable<ReviewResponse>> GetReviewsById(int recipeId)
        {
            var reviews = await _repository.GetReviewsByRecipeId(recipeId);
            return _mapper.Map<IEnumerable<ReviewResponse>>(reviews);
        }

    }
}
