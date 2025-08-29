using Personal.Shopping.Manager.Web.Models;
using Personal.Shopping.Manager.Web.Models.Coupon;
using Refit;

namespace Personal.Shopping.Manager.Web.Services.Interfaces;

public interface ICouponApiClient
{
    [Get("/api/coupon")]
    Task<ResponseDto> GetAllCuponsAsync();

    [Get("/api/coupon/get-by-code/{couponCode}")]
    Task<ResponseDto> GetCuponByCodeAsync(string couponCode);

    [Get("/api/coupon/get-by-id/{couponId}")]
    Task<ResponseDto> GetCuponByIdAsync(int couponId);

    [Post("/api/coupon")]
    Task<ResponseDto> CreateCouponAsync([Body] CouponDto coupon);

    [Put("/api/coupon")]
    Task<ResponseDto> UpdateCouponAsync([Body] CouponDto coupon);

    [Delete("/api/coupon/delete/{id}")]
    Task<ResponseDto> DeleteCouponAsync(int id);
}
