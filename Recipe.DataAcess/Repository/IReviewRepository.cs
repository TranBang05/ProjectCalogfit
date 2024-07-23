using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recipe.DataAcess.Models;

namespace Recipe.DataAcess.Repository
{
    public interface  IReviewRepository
    {
        Task<Review> AddReviewAsync(Review review);
        Task<IEnumerable<Review>> GetReviews();
        Task<IEnumerable<Review>> GetReviewsByRecipeId(int recipeId);
    }
}
