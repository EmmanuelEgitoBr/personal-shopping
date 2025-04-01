using Entities = Personal.Shopping.Services.Coupon.Domain.Entities;

namespace Personal.Shopping.Services.Coupon.Domain.Interfaces;

public interface ICouponRepository
{
    Task<Entities.Coupon> CreateCoupon(Entities.Coupon coupon);
    Task DeleteCoupon(int id);
    Task<IEnumerable<Entities.Coupon>> GetAllCoupons();
    Task<Domain.Entities.Coupon> GetCuponByCode(string couponCode);
    Task<Entities.Coupon> GetCouponsById(int couponId);
    Task<Entities.Coupon> UpdateCoupon(Entities.Coupon coupon);
}
