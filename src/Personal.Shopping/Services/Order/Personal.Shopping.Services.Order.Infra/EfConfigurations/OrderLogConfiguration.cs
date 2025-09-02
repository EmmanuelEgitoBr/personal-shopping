using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personal.Shopping.Services.Order.Domain.Entity;

namespace Personal.Shopping.Services.Order.Infra.EfConfigurations;

public class OrderLogConfiguration : IEntityTypeConfiguration<OrderLog>
{
    public void Configure(EntityTypeBuilder<OrderLog> builder)
    {
        // Nome da tabela (opcional)
        builder.ToTable("OrderLogs");

        // Chave primária
        builder.HasKey(o => o.Id);

        // Propriedades
        builder.Property(o => o.LogDescription)
               .HasMaxLength(500);

        builder.Property(o => o.UserId)
               .IsRequired();

        builder.Property(o => o.OrderDate)
               .IsRequired();
    }
}
