using Menu.API.Dto.Request;
using Menu.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Menu.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateMenu([FromBody] MenuRequest request)
        {
            if (request == null)
            {
                return BadRequest("Yêu cầu không hợp lệ");
            }

            try
            {
                var menuResponse = await _menuService.CreateMenuAsync(request);
                return Ok(menuResponse);
            }
            catch (ArgumentNullException ex)
            {
                // Xử lý trường hợp đối số null
                return BadRequest($"Yêu cầu không hợp lệ: {ex.Message}");
            }

        }

        
    }
}
