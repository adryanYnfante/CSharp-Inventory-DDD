using Dapper;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Infrastructure.Contexts;

namespace InventoryApp.Infrastructure.Repositories
{
    public class InvoiceRecordRepository : IInvoiceRecordRepository
    {
        private readonly InventoryDapperContext _dapperContext;

        public InvoiceRecordRepository(InventoryDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<List<Invoice>> GetAll()
        {
            var query = "SELECT Id, Date, IdType, IdClient, Invoices.Total, Cancelled, Subtotal, Tax " +
                        "FROM Invoices; " +
                        $"SELECT Id, ProductId ,InvoiceId, UnitPrice, Quantity, Subtotal, Tax, Total " +
                        $"FROM InvoiceDetails; " +
                        $"SELECT Id, Name, Enabled, InInventory, MinUnits, MaxUnits, Price " +
                        $"FROM Products ";

            using (var connection = _dapperContext.CreateConnection())
            {
                using (var results = await connection.QueryMultipleAsync(query))
                {
                    var invoices = results.Read<Invoice>().ToList();
                    var details = results.Read<InvoiceDetail>().ToList();
                    var products = results.Read<Product>().ToList();

                    details.ForEach(detail =>
                    {
                        detail.Product = products.SingleOrDefault(item => item.Id == detail.ProductId);
                    });

                    invoices.ForEach(invoice =>
                    {
                        invoice.InvoiceDetails = details.Where(detail => detail.InvoiceId == invoice.Id).ToList();
                    });

                    return invoices;
                }
            }
        }

        public async Task<Invoice> GetById(int invoiceId)
        {
            var query = $"SELECT Id, Date, IdType, IdClient, Invoices.Total, Cancelled, Subtotal, Tax " +
                        $"FROM Invoices " +
                        $"WHERE Id = @invoiceId; " +
                        $"SELECT Id, ProductId ,InvoiceId, UnitPrice, Quantity, Subtotal, Tax, Total " +
                        $"FROM InvoiceDetails " +
                        $"WHERE InvoiceId = @invoiceId;" +
                        $"SELECT Id, Name, Enabled, InInventory, MinUnits, MaxUnits, Price " +
                        $"FROM Products ";

            using (var connection = _dapperContext.CreateConnection())
            {
                using (var results = await connection.QueryMultipleAsync(query, new { invoiceId }))
                {
                    var invoice = results.Read<Invoice>().SingleOrDefault();
                    var details = results.Read<InvoiceDetail>().ToList();
                    var products = results.Read<Product>().ToList();

                    if (invoice != null && details != null)
                    {
                        invoice.InvoiceDetails = details;

                        invoice.InvoiceDetails.ForEach(detail =>
                        {
                            detail.Product = products.SingleOrDefault(item => item.Id == detail.ProductId);
                        });
                    }

                    return invoice;
                }
            }
        }

        public async Task<List<Invoice>> FindByField(string IdClient)
        {
            var query = "SELECT Id, Date, IdType, IdClient, Invoices.Total, Cancelled, Subtotal, Tax " +
                        "FROM Invoices " +
                        "WHERE IdClient = @IdClient;" +
                        $"SELECT Id, ProductId ,InvoiceId, UnitPrice, Quantity, Subtotal, Tax, Total " +
                        $"FROM InvoiceDetails; " +
                        $"SELECT Id, Name, Enabled, InInventory, MinUnits, MaxUnits, Price " +
                        $"FROM Products";

            using (var connection = _dapperContext.CreateConnection())
            {
                using (var results = await connection.QueryMultipleAsync(query, new { IdClient }))
                {
                    var invoices = results.Read<Invoice>().ToList();
                    var details = results.Read<InvoiceDetail>().ToList();
                    var products = results.Read<Product>().ToList();

                    details.ForEach(detail =>
                    {
                        detail.Product = products.SingleOrDefault(item => item.Id == detail.ProductId);
                    });

                    invoices.ForEach(invoice =>
                    {
                        invoice.InvoiceDetails = details.Where(detail => detail.InvoiceId == invoice.Id).ToList();
                    });

                    return invoices;
                }
            }
        }
    }
}