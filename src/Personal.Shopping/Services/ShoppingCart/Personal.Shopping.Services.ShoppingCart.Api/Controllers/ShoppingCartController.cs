using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.ShoppingCart.Application.Dtos;

namespace Personal.Shopping.Services.ShoppingCart.Api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        [HttpPost("upsert")]
        public IActionResult CartUpsert(CartDto cartDto)
        {
            return Ok();
        }
    }
}
