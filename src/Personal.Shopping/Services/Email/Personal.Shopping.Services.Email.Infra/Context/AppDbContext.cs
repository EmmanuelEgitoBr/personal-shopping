using Microsoft.EntityFrameworkCore;
using Personal.Shopping.Services.Email.Domain.Entities;

namespace Personal.Shopping.Services.Email.Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<EmailLogger> EmailLoggers { get; set; }
}
