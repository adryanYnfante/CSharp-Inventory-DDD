using InventoryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApp.Infrastructure.Configs
{
    public class InvoiceDetailConfig : IEntityTypeConfiguration<InvoiceDetail>
    {
        public void Configure(EntityTypeBuilder<InvoiceDetail> builder)
        {
            builder.ToTable("InvoiceDetails");

            builder.HasKey(key => new { key.Id });

            builder.Property(field => field.Id)
                .UseIdentityColumn();

            builder.Property(field => field.UnitPrice)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Quantity)
                .IsRequired();

            builder.Property(field => field.Tax)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Total)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .HasOne(detail => detail.Product)
                .WithMany(product => product.InvoiceDetails);

            builder
                .HasOne(detail => detail.Invoice)
                .WithMany(invoice => invoice.InvoiceDetails);
        }
    }
}