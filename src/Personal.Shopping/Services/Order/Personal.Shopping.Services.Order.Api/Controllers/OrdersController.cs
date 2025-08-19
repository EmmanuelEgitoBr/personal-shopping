using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Dtos.Stripe;
using Personal.Shopping.Services.Order.Application.Interfaces;
using Stripe.Checkout;

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

        //[Authorize]
        [HttpPost("create-order")]
        public async Task<ResponseDto> CreateOrder([FromBody]CartDto cartDto)
        {
            var response = await _orderService.CreateOrderAsync(cartDto);
            return response;
        }

        //[Authorize]
        [HttpPost("create-stripe-session")]
        public async Task<ResponseDto> CreateStripeSession([FromBody] StripeRequestDto stripeRequestDto)
        {
            try
            {
                var options = new SessionCreateOptions
                {
                    Mode = "payment",
                    SuccessUrl = stripeRequestDto.ApprovedUrl,
                    CancelUrl = stripeRequestDto.CancelUrl,
                    LineItems = new List<SessionLineItemOptions>()
                };

                foreach (var item in stripeRequestDto.OrderHeader!.OrderDetails!)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "brl",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product!.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                stripeRequestDto.StripeSessionUrl = session.Url;

                var orderHeaderDto = await _orderService.GetOrderHeaderByIdAsync(stripeRequestDto.OrderHeader.OrderHeaderId);
                orderHeaderDto.StripeSessionId = session.Id;

                await _orderService.UpdateOrderHeaderAsync(orderHeaderDto);

                return new ResponseDto
                {
                    IsSuccess = true,
                    Result = stripeRequestDto
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
