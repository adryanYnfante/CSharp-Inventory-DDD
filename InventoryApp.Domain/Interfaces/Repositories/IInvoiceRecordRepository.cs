using InventoryApp.Domain.Entities;

namespace InventoryApp.Domain.Interfaces.Repositories
{
    public interface IInvoiceRecordRepository : IRecordRepository<Invoice, int, string>
    {
    }
}