using Microsoft.AspNetCore.Mvc;
using Personal.Shopping.OrderManager.Web.Models;
using Personal.Shopping.OrderManager.Web.Models.ShoppingCart;
using Personal.Shopping.OrderManager.Web.Models.Stripe;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface IOrderApiClient
{
    [Post("/api/orders/create-order")]
    Task<ResponseDto> CreateOrderAsync([Body]CartDto cartDto);

    [Post("/api/orders/create-stripe-session")]
    Task<ResponseDto> CreateStripeSessionAsync([Body]StripeRequestDto stripeRequestDto);

    [Get("/api/orders/get-all-orders")]
    Task<ResponseDto> GetAllOrders();

    [Get("/api/orders/get-order/{orderHeaderId}")]
    Task<ResponseDto> GetOrderByOrderHeaderId(int orderHeaderId);

    [Get("/api/orders/get-orders/{userId}")]
    Task<ResponseDto> GetOrdersByUserId(string userId);

    [Get("/api/orders/{orderHeaderId}/update-status?newOrderStatus={newOrderStatus}")]
    Task<ResponseDto> UpdateOrderStatus([FromQuery] string newOrderStatus, int orderHeaderId);

    [Post("/api/orders/validate-stripe-session")]
    Task<ResponseDto> ValidateStripeSessionAsync([Body] int orderHeaderId);
}
