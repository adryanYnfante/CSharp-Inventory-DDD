using InventoryApp.Domain.Configuration;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Exceptions;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Domain.UseCases.Interfaces;
using Microsoft.Extensions.Options;
using System.Linq;

namespace InventoryApp.Domain.UseCases
{
    public class InvoiceUseCase : ICreateInvoiceUseCase
    {
        private readonly ICreateInvoiceRepository _invoiceRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOptions<AppSettings> _configuration;

        public InvoiceUseCase(
            ICreateInvoiceRepository invoiceRepository,
            IProductRepository productRepository,
            IOptions<AppSettings> configuration)
        {
            _invoiceRepository = invoiceRepository;
            _productRepository = productRepository;
            _configuration = configuration;
        }

        public async Task<Invoice> Add(Invoice invoice)
        {
            invoice.Date = DateTime.Now;
            invoice.Cancelled = false;

            var products = await _productRepository
                .GetByCondition(product => invoice.InvoiceDetails.Select(invoice => invoice.ProductId)
                .Contains(product.Id));

            invoice.InvoiceDetails.ForEach(detail =>
            {
                var productSelected = products.First(product => product.Id == detail.ProductId);

                ValidateProductExist(productSelected);
                ValidateProductIsEnabled(productSelected);
                ValidateStock(detail.Quantity, productSelected);

                CalculateInvoiceDetailValues(detail, productSelected);

                productSelected.DecreaseInventory(detail.Quantity);
                _productRepository.Update(productSelected);

                CalculateInvoiceValues(invoice, detail);
            });

            Invoice invoiceCreated = await _invoiceRepository.Add(invoice);
            await _productRepository.UnitOfWork.SaveChangesAsync();
            await _invoiceRepository.UnitOfWork.SaveChangesAsync();
            return invoiceCreated;
        }

        public async Task<Invoice> Cancel(int invoiceId)
        {
            var invoiceSelected = await _invoiceRepository.GetById(invoiceId);
            invoiceSelected.Cancelled = true;
            _invoiceRepository.Update(invoiceSelected);
            await _invoiceRepository.UnitOfWork.SaveChangesAsync();
            return invoiceSelected;
        }

        private void ValidateProductIsEnabled(Product productSelected)
        {
            if (!productSelected.IsEnable())
                throw new BusinessException("The selected product is not enabled for sale.");
        }

        private void ValidateProductExist(Product? productSelected)
        {
            if ((productSelected is null))
                throw new BusinessException("The product selected does not exist, please verify and try again.");
        }

        private void ValidateStock(int quantity, Product productSelected)
        {
            if (!productSelected.InStock(quantity))
                throw new BusinessException("There are not enough units in inventory to complete the sale.");
        }

        private void CalculateInvoiceDetailValues(InvoiceDetail detail, Product productSelected)
        {
            detail.UnitPrice = productSelected.Price;
            detail.CalculateSubtotal();
            detail.CalculateTax(_configuration.Value.Tax);
            detail.CalculateTotal();
        }

        private void CalculateInvoiceValues(Invoice invoice, InvoiceDetail detail)
        {
            invoice.Subtotal += detail.Subtotal;
            invoice.Tax += detail.Tax;
            invoice.Total += detail.Total;
        }
    }
}