using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Calofit_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Calofit_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly HttpClient _httpClient;
        public UserController(ILogger<UserController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
        }
        private bool IsUserLoggedIn()
        {
            var token = HttpContext.Session.GetString("JWToken");
            if (token == null)
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
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
        public async Task<IActionResult> Update(int id)
        {
            var url = $"http://localhost:5074/api/User/{id}";
            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var s = JsonConvert.DeserializeObject<User>(data);
                return View(s);
            }
            else
            {
                ViewBag.ErrorMessage = "Không thể lấy thông tin User để chỉnh sửa.";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = $"http://localhost:5195/api/User";
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                    var token = HttpContext.Session.GetString("JWToken");
                    if (token != null)
                    {
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    var response = await _httpClient.PutAsync(apiUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("UserDetail");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to update user.";
                        return View(user);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calling API to update user");
                    ViewBag.ErrorMessage = $"Error: {ex.Message}";
                    return View(user);
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide valid user details.";
                return View(user);
            }
        }

        public IActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var apiUrl = $"http://localhost:5074/api/User/{id}";
                var token = HttpContext.Session.GetString("JWToken");
                if (token != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }

                var response = await _httpClient.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("UserDetail");
                }
                else
                {
                    ViewBag.ErrorMessage = "Failed to delete user.";
                    return RedirectToAction("UserDetail");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while calling API to delete user");
                ViewBag.ErrorMessage = $"Error: {ex.Message}";
                return RedirectToAction("UserDetail");
            }
        }

    private async Task<List<SelectListItem>> GetRoleAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5074/api/Role");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var roles = JsonConvert.DeserializeObject<List<RoleViewModel>>(data);

            var roleList = roles?.Select(r => new SelectListItem
            {
                Value = r.RoleId.ToString(),
                Text = r.RoleName
            }).ToList() ?? new List<SelectListItem>();

            return roleList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving roles: {ex.Message}");
            return new List<SelectListItem>();
        }
    }

    private async Task<List<SelectListItem>> GetPermissionAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("http://localhost:5074/api/Permision");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var permissions = JsonConvert.DeserializeObject<List<PermissionViewModel>>(data);

            var permissionList = permissions?.Select(p => new SelectListItem
            {
                Value = p.PermissionId.ToString(),
                Text = p.PermissionName
            }).ToList() ?? new List<SelectListItem>();

            return permissionList;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving permissions: {ex.Message}");
            return new List<SelectListItem>();
        }
    }
        public async Task<IActionResult> Create()
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("/Home/Index");
            }

            try
            { // Lấy danh sách vai trò và quyền từ các API
                var roles = await GetRoleAsync();
                var permissions = await GetPermissionAsync();


                // Tạo một danh sách quyền cho từng vai trò
                var roleViewModels = roles.Select(role => new RoleViewModel
                {
                    RoleId = int.Parse(role.Value),
                    RoleName = role.Text,
                    Permissions = permissions.Select(p => new PermissionViewModel
                    {
                        PermissionId = int.Parse(p.Value),
                        PermissionName = p.Text
                    }).ToList()
                }).ToList();

                var viewModel = new CreateUserViewModel
                {
                    Roles = roleViewModels
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error loading Create page: {ex.Message}", ex);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (!IsUserLoggedIn())
            {
                return RedirectToAction("Login");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var apiUrl = "http://localhost:5074/api/User";
                    var jsonContent = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(apiUrl, jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        ViewBag.SuccessMessage = "User created successfully.";
                        return RedirectToAction("UserDetail");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Failed to create user.";
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error while calling API to create user");
                    ViewBag.ErrorMessage = $"Error: {ex.Message}";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please provide valid user details.";
            }

            ViewBag.Roles = await GetRoleAsync();
            ViewBag.Permissions = await GetPermissionAsync();
            return View(model);
        }

        


    }
}
