using InventoryApp.Domain.Entities;

namespace InventoryApp.Domain.UseCases.Interfaces
{
    public interface ICreateInvoiceUseCase
    {
        Task<Invoice> Add(Invoice invoice);

        Task<Invoice> Cancel(int invoiceId);
    }
}