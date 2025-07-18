using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Services.Email.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailLoggerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok("Olá!!");
        }
    }
}
