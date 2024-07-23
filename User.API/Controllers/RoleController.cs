using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Dto.Response;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {

        private readonly IUserService _userService;

        public RoleController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        //[Authorize(Policy = "Admin")]
        public async Task<ActionResult<IEnumerable<RoleResponse>>> GetUsers()
        {
            var u = await _userService.GetRole();
            return Ok(u);
        }
    }
}
