using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Models.Stripe;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
    Task<ResponseDto> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto);
}
