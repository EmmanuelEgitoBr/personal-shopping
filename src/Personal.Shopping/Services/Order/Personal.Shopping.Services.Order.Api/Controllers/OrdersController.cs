using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Dtos.Stripe;
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

        [HttpGet("get-all-orders")]
        public async Task<ResponseDto> GetAllOrders() 
        {
            return await _orderService.GetAllOrderHeadersAsync();
        }

        [HttpGet("get-orders/{userId}")]
        public async Task<ResponseDto> GetOrdersByUserId(string userId)
        {
            return await _orderService.GetOrderHeadersByUserIdAsync(userId);
        }

        [HttpGet("get-order/{orderHeaderId:int}")]
        public async Task<ResponseDto> GetOrderByOrderHeaderId(int orderHeaderId)
        {
            return await _orderService.GetOrderHeaderByIdAsync(orderHeaderId);
        }

        //[Authorize]
        [HttpPost("create-order")]
        public async Task<ResponseDto> CreateOrder([FromBody]CartDto cartDto)
        {
            return await _orderService.CreateOrderAsync(cartDto);
        }

        //[Authorize]
        [HttpPost("create-stripe-session")]
        public async Task<ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            return await _orderService.CreateStripeSessionAsync(stripeRequestDto);
        }

        [HttpPost("validate-stripe-session")]
        public async Task<ResponseDto> ValidateStripeSession([FromBody] int orderHeaderId)
        {
            return await _orderService.ValidateStripeSessionAsync(orderHeaderId);
        }

        [HttpPut("{orderHeaderId:int}/update-status")]
        public async Task<ResponseDto> UpdateOrderStatus([FromQuery]string newOrderStatus, int orderHeaderId)
        {
            return await _orderService.UpdateOrderStatusAsync(newOrderStatus, orderHeaderId);
        }
    }
}
