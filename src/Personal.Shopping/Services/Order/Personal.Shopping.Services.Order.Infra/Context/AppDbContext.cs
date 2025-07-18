using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<OrderHeader> OrderHeaders { get; set; }
}
