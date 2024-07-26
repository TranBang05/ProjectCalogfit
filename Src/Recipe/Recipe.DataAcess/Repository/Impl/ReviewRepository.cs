using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Recipe.DataAcess.Models;

namespace Recipe.DataAcess.Repository.Impl
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly RecipeDbContext _context;

        public ReviewRepository(RecipeDbContext context)
        {
            _context = context;
        }

        public async Task<Review> AddReviewAsync(Review review)
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviewsByRecipeId(int recipeId)
        {
            return await _context.Reviews
                .Where(r => r.RecipeId == recipeId)
                .ToListAsync();

        }
    }
}
