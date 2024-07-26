namespace Calofit_User.Models
{
	public class Review
	{
        public int Rating { get; set; }
        public int RecipeId { get; set; }
        public string? Comment { get; set; }
        public int UserId { get; set; }
        public string? UserName {  get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
