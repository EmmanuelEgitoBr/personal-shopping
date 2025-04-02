using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Services.Auth.Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser()
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
