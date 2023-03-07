using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Domain.UseCases.Interfaces;

namespace InventoryApp.Domain.UseCases
{
    public class RecordUseCase : IInvoiceRecordUseCase
    {
        private readonly IInvoiceRecordRepository _repository;

        public RecordUseCase(IInvoiceRecordRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Invoice>> FindByClientId(string clientId)
        {
            return await _repository.FindByField(clientId);
        }

        public async Task<List<Invoice>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Invoice> GetById(int invoiceId)
        {
            return await _repository.GetById(invoiceId);
        }
    }
}