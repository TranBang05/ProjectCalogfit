using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Calofit_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Calofit_MVC.Controllers
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

        public IActionResult Admin()
        {
            return View();
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
        
            public async Task<IActionResult> UserDetail()
        {
            try
            {
                var url = "http://localhost:5074/api/User";
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                var token = HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
                var response = await _httpClient.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    var subjects = JsonConvert.DeserializeObject<List<User>>(data);
                    return View(subjects);
                }
                else if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    ViewBag.ErrorMessage = "Bạn không có quyền truy cập vào tài nguyên này.";
                    return View(new List<User>());
                }
                else
                {
                    ViewBag.ErrorMessage = "Đã xảy ra lỗi khi truy cập tài nguyên.";
                    return View(new List<User>());
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi gọi API");
                ViewBag.ErrorMessage = $"Lỗi: {ex.Message}";
                return View(new List<User>());
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
