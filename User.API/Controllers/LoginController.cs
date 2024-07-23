using Microsoft.AspNetCore.Mvc;
using User.API.Dto.Request;
using User.API.Services;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthRequest request)
        {
            var response = await _loginService.Authenticate(request.Email, request.Password);
            if (!response.Successful) return Unauthorized();

            return Ok(response);
        }


    }
}
