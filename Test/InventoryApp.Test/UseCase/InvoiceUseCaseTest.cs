using InventoryApp.Domain.Configuration;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Test.Fixture;
using InventoryApp.Domain.UseCases;
using Microsoft.Extensions.Options;
using Moq;
using InventoryApp.Domain.Interfaces.Repositories.Base;
using System.Linq.Expressions;

namespace InventoryApp.Test.UseCase
{
    public class InvoiceUseCaseTest
    {
        private readonly AppSettings _appSettings;

        public InvoiceUseCaseTest()
        {
            _appSettings = new AppSettings
            {
                Tax = 0.19M
            };
        }

        [Fact]
        public async Task CancelInvoice_ShouldReturnInvoiceCancelledTrue()
        {
            //Arrange
            var mockInvoiceRepository = new Mock<ICreateInvoiceRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockOptions = new Mock<IOptions<AppSettings>>();

            var saveChangesCalled = false;

            mockInvoiceRepository
                .Setup(repository => repository.GetById(It.IsAny<int>()))
                .ReturnsAsync(InvoiceFixture.GetOneInvoice());

            mockInvoiceRepository
                .Setup(repository => repository.Update(It.IsAny<Invoice>()))
                .Returns(InvoiceFixture.GetOneInvoice());

            mockInvoiceRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesCalled = true);

            var invoiceUseCase =
                new InvoiceUseCase(mockInvoiceRepository.Object, mockProductRepository.Object, mockOptions.Object);

            //Act
            var result = await invoiceUseCase.Cancel(It.IsAny<int>());

            //Assert
            Assert.True(result.Cancelled);
            Assert.True(saveChangesCalled);
        }

        [Fact]
        public async Task CreateInvoice_OnSuccess_ValidateTimesRepositoriesAreUsed()
        {
            //Arrange
            var mockInvoiceRepository = new Mock<ICreateInvoiceRepository>();
            var mockProductRepository = new Mock<IProductRepository>();
            var mockConfig = new Mock<IOptions<AppSettings>>();

            var saveChangesCalled = false;
            var saveChangesInvoiceCalled = false;

            mockProductRepository
                .Setup(repository => repository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(ProductsFixture.GetProductsTest);

            mockProductRepository
                .Setup(repository => repository.Update(It.IsAny<Product>()))
                .Returns(ProductsFixture.GetOneProduct);

            mockConfig
                .Setup(config => config.Value)
                .Returns(_appSettings);

            mockProductRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesCalled = true);

            mockInvoiceRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesInvoiceCalled = true);

            var invoiceUseCase =
                new InvoiceUseCase(mockInvoiceRepository.Object, mockProductRepository.Object, mockConfig.Object);

            //Act
            await invoiceUseCase.Add(InvoiceFixture.DataToCreateInvoice());

            //Assert
            Assert.True(saveChangesCalled);
            Assert.True(saveChangesInvoiceCalled);
            mockInvoiceRepository.Verify(mock => mock.Add((It.IsAny<Invoice>())), Times.Once());
            mockProductRepository.Verify(mock => mock.Update((It.IsAny<Product>())), Times.Exactly(2));
            mockProductRepository.Verify(mock => mock.GetByCondition((It.IsAny<Expression<Func<Product, bool>>>())), Times.Once());
        }
    }
}