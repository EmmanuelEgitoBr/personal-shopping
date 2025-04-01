using Personal.Shopping.Services.Coupon.Application.Dtos;

namespace Personal.Shopping.Services.Coupon.Application.Interfaces
{
    public interface ICouponService
    {
        Task<ResponseDto<CouponDto>> CreateCouponAsync(CouponDto coupon);
        Task DeleteCouponAsync(int id);
        Task<ResponseDto<IEnumerable<CouponDto>>> GetAllCuponsAsync();
        Task<ResponseDto<CouponDto>> GetCuponByCodeAsync(string couponCode);
        Task<ResponseDto<CouponDto>> GetCuponByIdAsync(int couponId);
        Task<ResponseDto<CouponDto>> UpdateCouponAsync(CouponDto coupon);
    }
}
