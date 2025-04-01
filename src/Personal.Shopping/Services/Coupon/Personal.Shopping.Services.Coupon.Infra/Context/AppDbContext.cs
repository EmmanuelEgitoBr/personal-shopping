using Microsoft.EntityFrameworkCore;
using Models = Personal.Shopping.Services.Coupon.Domain.Entities;

namespace Personal.Shopping.Services.Coupon.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Coupon> Coupons { get; set; }
}
