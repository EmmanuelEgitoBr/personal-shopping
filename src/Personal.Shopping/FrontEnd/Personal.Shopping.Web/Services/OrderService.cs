using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.ShoppingCart;

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
}
