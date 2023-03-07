using InventoryApp.Domain.Entities;

namespace InventoryApp.Domain.UseCases.Interfaces
{
    public interface IInvoiceRecordUseCase
    {
        Task<List<Invoice>> FindByClientId(string clientId);

        Task<List<Invoice>> GetAll();

        Task<Invoice> GetById(int invoiceId);
    }
}