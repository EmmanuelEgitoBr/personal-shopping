using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Models.Stripe;
using Microsoft.AspNetCore.Mvc;

namespace Personal.Shopping.Web.Services;

public class OrderService : IOrderService
{
    private readonly IBaseService _baseService;

    public OrderService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateOrderAsync(CartDto cartDto)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = cartDto,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/create-order"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> CreateStripeSessionAsync(StripeRequestDto stripeRequestDto)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = stripeRequestDto,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/create-stripe-session"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> GetAllOrders()
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/get-all-orders"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> GetOrderByOrderHeaderId(int orderHeaderId)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/get-order/{orderHeaderId}"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> GetOrdersByUserId(string userId)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/get-orders/{userId}"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> UpdateOrderStatus([FromQuery] int newOrderStatus, int orderHeaderId)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.PUT,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/{orderHeaderId}/update-status?newOrderStatus={newOrderStatus}"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> ValidateStripeSessionAsync(int orderHeaderId)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = orderHeaderId,
            Url = AppSettings.OrderBaseUrl + $"/api/orders/validate-stripe-session"
        };
        return await _baseService.SendAsync(request!)!;
    }
}
