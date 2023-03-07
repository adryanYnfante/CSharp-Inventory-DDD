using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories.Base;

namespace InventoryApp.Domain.Interfaces.Repositories
{
    public interface ICreateInvoiceRepository : ICommandRepository<Invoice, int>
    { }
}