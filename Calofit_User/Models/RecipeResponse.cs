namespace Calofit_User.Models
{
	public class RecipeResponse
	{
		public int RecipeId { get; set; }
		public string? Name { get; set; }
		public string? Description { get; set; }
		public string? Image { get; set; }
        public List<Ingredient>? Ingredients { get; set; }
        public List<Review>? Reviews { get; set; }
	}
}
