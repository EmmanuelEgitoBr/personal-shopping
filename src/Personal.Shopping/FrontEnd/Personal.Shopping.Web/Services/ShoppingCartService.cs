using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;
using Personal.Shopping.Web.Models.ShoppingCart;
using Personal.Shopping.Web.Services.Interfaces;

namespace Personal.Shopping.Web.Services;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IBaseService _baseService;

    public ShoppingCartService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CartUpsertAsync(CartDto cart)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = cart,
            Url = AppSettings.ShoppingCartBaseUrl + $"/api/cart/upsert-cart"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> RemoveCartAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = id,
            Url = AppSettings.ShoppingCartBaseUrl + $"/api/cart/remove-cart"
        });
    }

    public async Task<ResponseDto> GetCartByUserIdAsync(string userId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.ShoppingCartBaseUrl + $"/api/cart/get-cart/{userId}"
        });
    }

    public async Task<ResponseDto> ApplyCouponAsync(CartDto cart)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = cart,
            Url = AppSettings.ShoppingCartBaseUrl + $"/api/cart/apply-coupon"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> RemoveCouponAsync(CartDto cart)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = cart,
            Url = AppSettings.ShoppingCartBaseUrl + $"/api/cart/remove-coupon"
        });
    }
}
