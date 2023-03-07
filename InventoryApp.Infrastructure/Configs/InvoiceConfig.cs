using InventoryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryApp.Infrastructure.Configs
{
    public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.ToTable("Invoices");

            builder.HasKey(key => key.Id);

            builder.Property(field => field.Id)
                .UseIdentityColumn();

            builder.Property(field => field.Date)
                .HasColumnType("datetime");

            builder.Property(field => field.IdType)
                .HasMaxLength(5)
                .IsRequired();

            builder.Property(field => field.IdClient)
                .HasMaxLength(15)
                .IsRequired();

            builder.Property(field => field.Subtotal)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Tax)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Total)
                .HasPrecision(18, 2)
                .IsRequired();

            builder.Property(field => field.Cancelled)
                .HasDefaultValue(false)
                .IsRequired();

            builder
                .HasMany(invoice => invoice.InvoiceDetails)
                .WithOne(detail => detail.Invoice);
        }
    }
}