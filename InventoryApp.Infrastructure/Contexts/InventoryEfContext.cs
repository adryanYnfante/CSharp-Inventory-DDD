using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Infrastructure.Contexts
{
    public class InventoryEfContext : DbContext, IUnitOfWork
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public InventoryEfContext(DbContextOptions<InventoryEfContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryEfContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}