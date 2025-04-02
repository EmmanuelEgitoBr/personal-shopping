using Personal.Shopping.Web.Models;
using Personal.Shopping.Web.Models.Enums;
using Personal.Shopping.Web.Services.Interfaces;
using Personal.Shopping.Web.Services.Interfaces.Base;

namespace Personal.Shopping.Web.Services;

public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;

    public CouponService(IBaseService baseService,
        IConfiguration configuration)
    {
        _baseService = baseService;
        _configuration = configuration;
        _baseUrl = _configuration.GetValue<string>("ServicesUrls:CouponApi")!;
    }

    public async Task<ResponseDto> CreateCouponAsync(CouponDto coupon)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.POST,
            Content = coupon,
            Url = _baseUrl + $"/api/coupon"
        };
        return await _baseService.SendAsync(request!)!;
    }

    public async Task DeleteCouponAsync(int id)
    {
        await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.DELETE,
            Url = _baseUrl + $"/api/coupon/delete/{id}"
        });
    }

    public async Task<ResponseDto> GetAllCuponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _baseUrl + "/api/coupon"
        });
    }

    public async Task<ResponseDto> GetCuponByCodeAsync(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _baseUrl + $"/api/coupon/get-by-code/{couponCode}"
        });
    }

    public async Task<ResponseDto> GetCuponByIdAsync(int couponId)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = ApiType.GET,
            Url = _baseUrl + $"/api/coupon/get-by-id/{couponId}"
        });
    }

    public async Task<ResponseDto> UpdateCouponAsync(CouponDto coupon)
    {
        var request = new RequestDto()
        {
            ApiType = ApiType.PUT,
            Content = coupon,
            Url = _baseUrl + $"/api/coupon"
        };
        return await _baseService.SendAsync(request!)!;
    }
}
