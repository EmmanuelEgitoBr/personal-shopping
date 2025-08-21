using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Reward.Domain.Entities;

namespace Personal.Shopping.Services.Reward.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Rewards> Rewards { get; set; }
}
