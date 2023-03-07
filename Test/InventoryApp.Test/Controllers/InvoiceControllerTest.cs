using FluentAssertions;
using InventoryApp.Api.Controllers;
using InventoryApp.Domain.Entities;
using InventoryApp.Domain.UseCases.Interfaces;
using InventoryApp.Test.Fixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InventoryApp.Test.Controllers
{
    public class InvoiceControllerTest
    {
        private Mock<ICreateInvoiceUseCase> mockUseCase;
        private Mock<ILogger<InvoiceController>> mockLogger;

        [Fact]
        public async Task CreateInvoice_OnSuccess_ReturnsCreated201()
        {
            //Arrange
            mockUseCase = new Mock<ICreateInvoiceUseCase>();
            mockLogger = new Mock<ILogger<InvoiceController>>();

            mockUseCase
                .Setup(useCase => useCase.Add(It.IsAny<Invoice>()))
                .ReturnsAsync(InvoiceFixture.GetOneInvoice());

            var invoiceController = new InvoiceController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await invoiceController.CreateInvoice(It.IsAny<Invoice>());

            //Assert
            result.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)result;
            objectResult.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task CancelInvoice_OnSuccess_ReturnsOk()
        {
            //Arrange
            mockUseCase = new Mock<ICreateInvoiceUseCase>();
            mockLogger = new Mock<ILogger<InvoiceController>>();

            mockUseCase
                .Setup(useCase => useCase.Cancel(It.IsAny<int>()))
                .ReturnsAsync(InvoiceFixture.GetOneInvoice());

            var invoiceController = new InvoiceController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await invoiceController.CancelInvoice(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }
    }
}