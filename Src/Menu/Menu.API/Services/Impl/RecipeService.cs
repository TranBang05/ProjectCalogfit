using Menu.API.Dto.Request;
using Menu.API.Dto.Response;

namespace Menu.API.Services.Impl
{
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RecipeResponse>> GetRecipesByCategory(RecipeCategoryRequest categoryRequest)
        {
            if (categoryRequest == null || string.IsNullOrEmpty(categoryRequest.CategoryName))
            {
                throw new ArgumentNullException(nameof(categoryRequest), "Category request cannot be null or empty");
            }

            var requestUri = $"/api/Recipe/category?CategoryName={Uri.EscapeDataString(categoryRequest.CategoryName)}";

            // Kiểm tra BaseAddress và requestUri
            if (_httpClient.BaseAddress == null)
            {
                throw new InvalidOperationException("BaseAddress is not set for HttpClient.");
            }

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<RecipeResponse>>();
        }

        public async Task<IEnumerable<RecipeResponse>> GetRecipesByCategorys(string categoryName)
        {
            if (string.IsNullOrEmpty(categoryName))
            {
                throw new ArgumentNullException(nameof(categoryName), "Category name cannot be null or empty");
            }

            var requestUri = $"/api/Recipe/category?CategoryName={Uri.EscapeDataString(categoryName)}";

            var response = await _httpClient.GetAsync(requestUri);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<RecipeResponse>>();
        }
    }
}
