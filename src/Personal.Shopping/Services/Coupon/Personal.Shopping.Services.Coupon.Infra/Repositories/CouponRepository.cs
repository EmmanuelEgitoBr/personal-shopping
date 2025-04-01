using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Coupon.Domain.Entities;
using Personal.Shopping.Services.Coupon.Domain.Interfaces;
using Personal.Shopping.Services.Coupon.Infra.Context;

namespace Personal.Shopping.Services.Coupon.Infra.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly AppDbContext _db;

    public CouponRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<Domain.Entities.Coupon> CreateCoupon(Domain.Entities.Coupon coupon)
    {
        _db.Coupons.Add(coupon);
        await _db.SaveChangesAsync();

        return coupon;
    }

    public async Task<IEnumerable<Domain.Entities.Coupon>> GetAllCoupons()
    {
        return await _db.Coupons.ToListAsync();
    }

    public async Task<Domain.Entities.Coupon> GetCouponsById(int couponId)
    {
        return await _db.Coupons.FirstOrDefaultAsync(c => c.CouponId == couponId);
    }

    public async Task<Domain.Entities.Coupon> GetCuponByCode(string couponCode)
    {
        return await _db.Coupons.FirstOrDefaultAsync(c => c.CouponCode == couponCode);
    }

    public async Task<Domain.Entities.Coupon> UpdateCoupon(Domain.Entities.Coupon coupon)
    {
        _db.Coupons.Update(coupon);
        await _db.SaveChangesAsync();

        return coupon;
    }

    public async Task DeleteCoupon(int id)
    {
        var coupon = await _db.Coupons.FirstOrDefaultAsync(c => c.CouponId == id);
        _db.Coupons.Remove(coupon);
        await _db.SaveChangesAsync();
    }
}
