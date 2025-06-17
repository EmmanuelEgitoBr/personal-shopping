using Microsoft.EntityFrameworkCore;

namespace Personal.Shopping.Services.ShoppingCart.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    //public DbSet<Category> Categories { get; set; }
    //public DbSet<Entity.Product> Products { get; set; }
}
