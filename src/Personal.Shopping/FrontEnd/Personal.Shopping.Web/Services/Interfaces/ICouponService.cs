using Personal.Shopping.Web.Models;

namespace Personal.Shopping.Web.Services.Interfaces;

public interface ICouponService
{
    Task<ResponseDto> CreateCouponAsync(CouponDto coupon);
    Task<ResponseDto> DeleteCouponAsync(int id);
    Task<ResponseDto> GetAllCuponsAsync();
    Task<ResponseDto> GetCuponByCodeAsync(string couponCode);
    Task<ResponseDto> GetCuponByIdAsync(int couponId);
    Task<ResponseDto> UpdateCouponAsync(CouponDto coupon);
}
