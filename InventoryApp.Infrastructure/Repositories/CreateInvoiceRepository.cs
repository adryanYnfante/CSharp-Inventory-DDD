using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Infrastructure.Contexts;
using InventoryApp.Infrastructure.Repositories.Base;

namespace InventoryApp.Infrastructure.Repositories
{
    public class CreateInvoiceRepository : CommandRepository<Invoice, int>, ICreateInvoiceRepository
    {
        private readonly InventoryEfContext _efContext;

        public CreateInvoiceRepository(InventoryEfContext efContext) : base(efContext)
        {
            _efContext = efContext;
        }
    }
}