using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.DataAcess.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public int RecipeId { get; set; }
        public Recipe? Recipe { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
