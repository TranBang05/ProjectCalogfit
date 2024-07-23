namespace Recipe.API.Dto.Request
{
    public class ReviewRequest
    {
        public int Rating { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
        
    }
}
