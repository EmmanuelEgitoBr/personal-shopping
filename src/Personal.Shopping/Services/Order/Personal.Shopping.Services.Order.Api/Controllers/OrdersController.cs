using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Services.Order.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetTest()
        {
            return Ok("Olá!!");
        }
    }
}
