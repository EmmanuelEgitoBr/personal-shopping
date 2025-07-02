using Personal.Shopping.Services.ShoppingCart.Application.Dtos;
using Refit;

namespace Personal.Shopping.Services.ShoppingCart.Application.Interfaces;

public interface ICouponService
{
    [Get("/api/coupon/get-by-code/{couponCode}")]
    Task<CouponDto> GetCoupon(string couponCode);
}
