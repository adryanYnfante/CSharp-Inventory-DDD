using InventoryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApp.Infrastructure.Configs
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(key => key.Id);

            builder.Property(field => field.Id)
                .UseIdentityColumn();

            builder.Property(field => field.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(field => field.Enabled)
                .IsRequired();

            builder.Property(field => field.InInventory)
                .IsRequired();

            builder.Property(field => field.MinUnits)
                .IsRequired();

            builder.Property(field => field.MaxUnits)
                .IsRequired();

            builder.Property(field => field.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .HasMany(product => product.InvoiceDetails)
                .WithOne(detail => detail.Product);
        }
    }
}