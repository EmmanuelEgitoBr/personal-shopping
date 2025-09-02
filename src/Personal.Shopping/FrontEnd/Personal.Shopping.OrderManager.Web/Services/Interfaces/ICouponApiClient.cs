using Personal.Shopping.OrderManager.Web.Models;
using Refit;

namespace Personal.Shopping.OrderManager.Web.Services.Interfaces;

public interface ICouponApiClient
{
    [Get("/api/coupon")]
    Task<ResponseDto> GetAllCuponsAsync();

    [Get("/api/coupon/get-by-code/{couponCode}")]
    Task<ResponseDto> GetCuponByCodeAsync(string couponCode);

    [Get("/api/coupon/get-by-id/{couponId}")]
    Task<ResponseDto> GetCuponByIdAsync(int couponId);
}
