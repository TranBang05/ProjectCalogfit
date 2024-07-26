using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Calofit_User.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Calofit_User.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromForm] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = "http://localhost:5074/api/Login"; // URL của API Login
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    // Gửi request POST đến API
                    var response = await _httpClient.PostAsync(apiUrl, jsonContent);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();
                        var authResponse = JsonConvert.DeserializeObject<AuthResponse>(responseData);

                        HttpContext.Session.SetString("JWToken", authResponse.Token);
                        if (authResponse.Roles != null && authResponse.Roles.Contains("Admin"))
                        {
                            return RedirectToAction("Admin", "Home");
                        }
                        else
                        {
                            return RedirectToAction("User", "Home");
                        }

                    }
                    else if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        ViewBag.ErrorMessage = "Invalid email or password.";
                        return View();
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to authenticate.";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calling API Login");
                    ViewBag.ErrorMessage = $"Error: {ex.Message}";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide valid credentials.";
                return View();
            }
        }

        public IActionResult User()
        {
            return View();
        }

		public async Task<IActionResult> Recipe()
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

        public async Task<IActionResult> RecipeDetail(int id)
        {
            var url = $"http://localhost:5298/api/Recipe/{id}"; 
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var recipe = JsonConvert.DeserializeObject<RecipeResponse>(data);
                return View(recipe);
            }
            else
            {
                ViewBag.ErrorMessage = "Failed to retrieve recipe details.";
                return View();
            }
        }



		[HttpPost]
		public async Task<IActionResult> AddReview(Review reviewModel)
		{
			if (ModelState.IsValid)
			{
				try
				{
					var token = HttpContext.Session.GetString("JWToken");
					if (string.IsNullOrEmpty(token))
					{
						ViewBag.ErrorMessage = "Bạn cần đăng nhập trước khi đánh giá món ăn!";
						return RedirectToAction("RecipeDetail", new { id = reviewModel.RecipeId });
					}

					var apiUrl = $"http://localhost:5298/api/Review/{reviewModel.RecipeId}";
					var jsonContent = new StringContent(JsonConvert.SerializeObject(reviewModel), Encoding.UTF8, "application/json");
					_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

					var response = await _httpClient.PostAsync(apiUrl, jsonContent);

					if (response.IsSuccessStatusCode)
					{
						return RedirectToAction("RecipeDetail", new { id = reviewModel.RecipeId });
					}
					else
					{
						ViewBag.ErrorMessage = "Failed to add review.";
						return RedirectToAction("RecipeDetail", new { id = reviewModel.RecipeId });
					}
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Error while calling API to add review");
					ViewBag.ErrorMessage = $"Error: {ex.Message}";
					return RedirectToAction("RecipeDetail", new { id = reviewModel.RecipeId });
				}
			}
			else
			{
				ViewBag.ErrorMessage = "Please provide valid review data.";
				return RedirectToAction("RecipeDetail", new { id = reviewModel.RecipeId });
			}
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
