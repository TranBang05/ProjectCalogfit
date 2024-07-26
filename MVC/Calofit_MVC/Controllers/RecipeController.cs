using System.Text;
using Calofit_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Calofit_MVC.Controllers
{
    public class RecipeController : Controller
    {

        private readonly ILogger<RecipeController> _logger;
        private readonly HttpClient _httpClient;
        public RecipeController(ILogger<RecipeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

       
        public IActionResult Create()
        {
            return View(new RecipeResponse());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RecipeResponse viewModel)
        {
            try
            {
                var url = "http://localhost:5074/api-recipes/Recipe";
                var jsonContent = new StringContent(JsonConvert.SerializeObject(viewModel), Encoding.UTF8, "application/json");

                // Log JSON content for debugging
                _logger.LogInformation("JSON Content: " + jsonContent.ReadAsStringAsync().Result);

                var response = await _httpClient.PostAsync(url, jsonContent);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("RecipeDetail");
                else
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Failed to create recipe. Status Code: {response.StatusCode}, Response: {responseContent}");
                    ViewBag.ErrorMessage = "Failed to create recipe.";
                    return View(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating recipe: {ex.Message}", ex);
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(viewModel);
            }
        }
        public async Task<IActionResult> RecipeDetail()
        {
            var url = "http://localhost:5298/api/Recipe";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var users = JsonConvert.DeserializeObject<List<RecipeResponse>>(data);
                return View(users);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve user list.";
                return View();
            }
        }
    }
}
