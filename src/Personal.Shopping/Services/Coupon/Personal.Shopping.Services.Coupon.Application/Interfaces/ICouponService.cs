using Personal.Shopping.Services.Coupon.Application.Dtos;

namespace Personal.Shopping.Services.Coupon.Application.Interfaces
{
    public interface ICouponService
    {
        Task<ResponseDto> CreateCouponAsync(CouponDto coupon);
        Task DeleteCouponAsync(int id);
        Task<ResponseDto> GetAllCuponsAsync();
        Task<ResponseDto> GetCuponByCodeAsync(string couponCode);
        Task<ResponseDto> GetCuponByIdAsync(int couponId);
        Task<ResponseDto> UpdateCouponAsync(CouponDto coupon);
        ResponseDto CreateCouponInStripe(CouponDto couponDto);
        ResponseDto DeleteCouponInStripe(CouponDto couponDto);
    }
}
