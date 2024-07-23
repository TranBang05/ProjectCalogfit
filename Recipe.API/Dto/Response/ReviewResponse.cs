namespace Recipe.API.Dto.Response
{
    public class ReviewResponse
    {
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
