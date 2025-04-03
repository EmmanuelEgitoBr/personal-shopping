using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Coupon;
using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;
using Personal.Shopping.Web.Settings;

namespace Personal.Shopping.Web.Services;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;

    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }

    public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = coupon,
            Url = AppSettings.CouponBaseUrl + $"/api/coupon"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task<ResponseDto> DeleteCouponAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = AppSettings.CouponBaseUrl + $"/api/coupon/delete/{id}"
        });
    }

    public async Task<ResponseDto> GetAllCuponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.CouponBaseUrl + "/api/coupon"
        });
    }

    public async Task<ResponseDto> GetCuponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.CouponBaseUrl + $"/api/coupon/get-by-code/{couponCode}"
        });
    }

    public async Task<ResponseDto> GetCuponByIdAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = AppSettings.CouponBaseUrl + $"/api/coupon/get-by-id/{couponId}"
        });
    }

    public async Task<ResponseDto> UpdateCouponAsync(CouponDto coupon)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.PUT,
            Content = coupon,
            Url = AppSettings.CouponBaseUrl + $"/api/coupon"
        };
        return await _baseService.SendAsync(request!)!;
    }
}
