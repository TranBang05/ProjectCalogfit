using AutoMapper;
using Recipe.API.Dto.Request;
using Recipe.API.Dto.Response;
using Recipe.DataAcess.Models;
using Recipe.DataAcess.Repository;

namespace Recipe.API.Service.Impl
{
    public class RecipeService : IRecipeService
    {
        private readonly IRecipeRepository _repository;
        private readonly IMapper _mapper;

        public RecipeService(IRecipeRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RecipeResponse>> GetAllRecipes()
        {  
            var recipes = await _repository.GetAllRecipes();
            var recipeResponses = _mapper.Map<IEnumerable<RecipeResponse>>(recipes);

            return recipeResponses;
        }

        public async Task<RecipeResponse> AddRecipeAsync(RecipeRequest request)
        {
            
            var recipe = _mapper.Map<DataAcess.Models.Recipe>(request);
            var addedRecipe = await _repository.CreateRecipe(recipe);

            var response = _mapper.Map<RecipeResponse>(addedRecipe);

            return response;
        }

        public async Task<RecipeResponse> GetRecipeById(int recipeId)
        {
            var recipe = await _repository.GetRecipeById(recipeId);

            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with ID {recipeId} not found.");
            }
            var response = _mapper.Map<RecipeResponse>(recipe);

            return response;
        }

        public async Task<IEnumerable<RecipeResponse>> GetRecipesByCategory(RecipeCategoryRequest categoryRequest)
        {
            if (categoryRequest == null)
            {
                throw new ArgumentNullException(nameof(categoryRequest));
            }

            // Chuyển đổi từ RecipeCategoryRequest sang RecipeCategory
            RecipeCategory category;
            if (!Enum.TryParse(categoryRequest.CategoryName, out category))
            {
                throw new ArgumentException("Invalid category", nameof(categoryRequest));
            }

            var recipes = await _repository.GetRecipesByCategory(category);
            return _mapper.Map<IEnumerable<RecipeResponse>>(recipes);
        }

        public async Task<IEnumerable<StepResponse>> GetStepResponses()
        {
            var step = await _repository.GetAllStep();
            var stepresponse = _mapper.Map<IEnumerable<StepResponse>>(step);

            return stepresponse;
        }

        public async Task<IEnumerable<IngredientResponse>> GetIngredientResponses()
        {
            var i = await _repository.GetAllIngredient();
            var iResponse = _mapper.Map<IEnumerable<IngredientResponse>>(i);

            return iResponse;
        }
    }
}
