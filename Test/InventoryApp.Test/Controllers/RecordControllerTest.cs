using InventoryApp.Api.Controllers;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.UseCases.Interfaces;
using InventoryApp.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InventoryApp.Test.Controllers
{
    public class RecordControllerTest
    {
        private Mock<IInvoiceRecordUseCase> mockUseCase;
        private Mock<ILogger<RecordController>> mockLogger;

        [Fact]
        public async Task GetAllInvoices_OnSuccess_ValidateIdClient()
        {
            //Arrange
            var expectedClientId = "1117963214";
            mockUseCase = new Mock<IInvoiceRecordUseCase>();
            mockLogger = new Mock<ILogger<RecordController>>();

            mockUseCase
                .Setup(useCase => useCase.GetAll())
                .ReturnsAsync(InvoiceFixture.GetInvoicesTest());

            var recordController = new RecordController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await recordController.GetAllInvoices();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Invoice>>(okResult.Value);
            var invoice = returnValue.FirstOrDefault();
            Assert.Equal(expectedClientId, invoice?.IdClient);
        }

        [Fact]
        public async Task GetInvoiceById_OnSuccess_ValidateClientType()
        {
            //Arrange
            var expectedClientType = "CC";
            mockUseCase = new Mock<IInvoiceRecordUseCase>();
            mockLogger = new Mock<ILogger<RecordController>>();

            mockUseCase
                .Setup(useCase => useCase.GetById(It.IsAny<int>()))
                .ReturnsAsync(InvoiceFixture.GetOneInvoice());

            var recordController = new RecordController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await recordController.GetInvoiceById(It.IsAny<int>());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var invoiceReturn = Assert.IsType<Invoice>(okResult.Value);
            Assert.Equal(expectedClientType, invoiceReturn?.IdType);
        }

        [Fact]
        public async Task GetInvoiceByClientId_OnSuccess_ValidateNumberOfInvoices()
        {
            //Arrange
            var expectedListSize = 2;
            mockUseCase = new Mock<IInvoiceRecordUseCase>();
            mockLogger = new Mock<ILogger<RecordController>>();

            mockUseCase
                .Setup(useCase => useCase.FindByClientId(It.IsAny<string>()))
                .ReturnsAsync(InvoiceFixture.GetInvoicesTest());

            var recordController = new RecordController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await recordController.GetInvoicesByClienteId(It.IsAny<string>());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var invoiceReturn = Assert.IsType<List<Invoice>>(okResult.Value);
            Assert.Equal(expectedListSize, invoiceReturn.Count);
        }
    }
}