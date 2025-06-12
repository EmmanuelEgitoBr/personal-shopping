using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Product.Domain.Entities;
using Entity = Personal.Shopping.Services.Product.Domain.Entities;

namespace Personal.Shopping.Services.Product.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Entity.Product> Products { get; set; }
}
