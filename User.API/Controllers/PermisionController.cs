using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Dto.Response;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermisionController : Controller
    {
        private readonly IUserService _userService;

        public PermisionController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<PermisionResponse>>> GetUsers()
        {
            var u = await _userService.GetPermision();
            return Ok(u);
        }
    }
    }
