using FluentAssertions;
using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Test.Fixture;
using InventoryApp.Domain.UseCases;
using Moq;

namespace InventoryApp.Test.UseCase
{
    public class RecordUseCaseTest
    {
        [Fact]
        public async Task GetAllInvoices__WhenCalled_RetursListOfExpectedSize()
        {
            //Arrange
            var mockRepository = new Mock<IInvoiceRecordRepository>();

            mockRepository
                .Setup(repository => repository.GetAll())
                .ReturnsAsync(InvoiceFixture.GetInvoicesTest());

            var recordUseCase = new RecordUseCase(mockRepository.Object);

            //Act
            var result = await recordUseCase.GetAll();

            //Assert
            result.Count.Should().Be(InvoiceFixture.GetInvoicesTest().Count);
        }

        [Fact]
        public async Task GetInvoiceById_WhenCalled_ReturnExpectedIdClient()
        {
            //Arrange
            var idClient = "1117963214";
            var mockRepository = new Mock<IInvoiceRecordRepository>();

            mockRepository
                .Setup(repository => repository.GetById(It.IsAny<int>()))
                .ReturnsAsync(InvoiceFixture.GetOneInvoice());

            var recordUseCase = new RecordUseCase(mockRepository.Object);

            //Act
            var result = await recordUseCase.GetById(It.IsAny<int>());

            //Assert
            Assert.Equal(idClient, result.IdClient);
        }

        [Fact]
        public async Task GetInvoicesByClientId_WhenCalled_ReturnExpectedListSize()
        {
            //Arrange
            var mockRepository = new Mock<IInvoiceRecordRepository>();

            mockRepository
                .Setup(repository => repository.FindByField(It.IsAny<string>()))
                .ReturnsAsync(InvoiceFixture.GetInvoicesTest());

            var recordUseCase = new RecordUseCase(mockRepository.Object);

            //Act
            var result = await recordUseCase.FindByClientId(It.IsAny<string>());

            //Assert
            result.Count.Should().Be(InvoiceFixture.GetInvoicesTest().Count);
        }
    }
}