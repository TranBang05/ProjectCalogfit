namespace Recipe.API.Dto.Response
{
    public class RecipeResponse
    {
        public int RecipeId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public List<IngredientResponse>? Ingredients { get; set;}
        public List<StepResponse>? Steps { get; set;}
        public List<ReviewResponse>? Reviews { get; set;}
    }
}
