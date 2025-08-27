using Personal.Shopping.Services.Order.Application.Dtos;
using Personal.Shopping.Services.Order.Application.Dtos.Cart;
using Personal.Shopping.Services.Order.Application.Dtos.Stripe;

namespace Personal.Shopping.Services.Order.Application.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> GetAllOrderHeadersAsync();
    Task<ResponseDto> GetOrderHeadersByUserIdAsync(string userId);
    Task<ResponseDto> GetOrderHeaderByIdAsync(int orderHeaderId);
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
    Task UpdateOrderHeaderAsync(OrderHeaderDto orderHeaderDto);
    Task<ResponseDto> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto);
    Task<ResponseDto> ValidateStripeSessionAsync(int orderHeaderId);
    Task<ResponseDto> UpdateOrderStatusAsync(string newOrderStatus, int orderHeaderId);
}
