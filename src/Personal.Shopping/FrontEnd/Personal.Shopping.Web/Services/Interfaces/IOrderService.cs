using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Models.Stripe;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface IOrderService
{
    Task<ResponseDto> CreateOrderAsync(CartDto cartDto);
    Task<ResponseDto> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto);
    Task<ResponseDto> ValidateStripeSessionAsync(int orderHeaderId);
    Task<ResponseDto> GetAllOrders();
    Task<ResponseDto> GetOrdersByUserId(string userId);
    Task<ResponseDto> GetOrderByOrderHeaderId(int orderHeaderId);
    Task<ResponseDto> UpdateOrderStatus([FromQuery] int newOrderStatus, int orderHeaderId);
}
