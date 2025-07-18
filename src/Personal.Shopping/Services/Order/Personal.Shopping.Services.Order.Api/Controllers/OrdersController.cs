using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Interfaces;

namespace Personal.Shopping.Services.Order.Api.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize]
        [HttpPost("create-order")]
        public async Task<ResponseDto> CreateOrder([FromBody]CartDto cartDto)
        {
            var response = await _orderService.CreateOrderAsync(cartDto);
            return response;
        }
    }
}
