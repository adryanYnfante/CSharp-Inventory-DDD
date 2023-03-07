using InventoryApp.Domain.Interfaces.Repositories;
using InventoryApp.Test.Fixture;
using InventoryApp.Domain.UseCases;
using Moq;
using FluentAssertions;
using InventoryApp.Domain.Entities;
using System.Linq.Expressions;

namespace InventoryApp.Test.UseCase
{
    public class ProductUseCaseTest
    {
        [Fact]
        public async Task GetAllProducts__WhenCalled_RetursListOfProductsOfExpectedSize()
        {
            //Arrange
            var mockRepository = new Mock<IProductRepository>();

            mockRepository
                .Setup(repository => repository.GetByCondition(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(ProductsFixture.GetProductsTest());

            var productUseCase = new ProductUseCase(mockRepository.Object);

            //Act
            var result = await productUseCase.GetAll();

            //Assert
            result.Count.Should().Be(ProductsFixture.GetProductsTest().Count);
        }

        [Fact]
        public async Task GetProductById_WhenCalled_ReturnExpectedProductName()
        {
            //Arrange
            var productName = "Test";
            var mockRepository = new Mock<IProductRepository>();

            mockRepository
                .Setup(repository => repository.GetById(It.IsAny<int>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            var productUseCase = new ProductUseCase(mockRepository.Object);

            //Act
            var result = await productUseCase.GetById(It.IsAny<int>());

            //Assert
            Assert.Equal(productName, result.Name);
        }

        [Fact]
        public async Task CreateProduct_WhenCalled_ShouldReturnProductCreated()
        {
            //Arrange
            var expectedIdCreated = 1;
            var mockRepository = new Mock<IProductRepository>();
            var saveChangesCalled = false;

            mockRepository
                .Setup(repository => repository.Add(It.IsAny<Product>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            mockRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesCalled = true);

            var productUseCase = new ProductUseCase(mockRepository.Object);

            //Act
            var result = await productUseCase.Add(It.IsAny<Product>());

            //Assert
            result.Should().BeOfType<Product>();
            Assert.Equal(expectedIdCreated, result.Id);
            Assert.True(saveChangesCalled);
        }

        [Fact]
        public async Task UpdateProduct_WhenCalled_ShouldReturnProductUpdated()
        {
            //Arrange
            var mockRepository = new Mock<IProductRepository>();
            var saveChangesCalled = false;

            mockRepository
                .Setup(repository => repository.Update(It.IsAny<Product>()))
                .Returns(ProductsFixture.GetOneProduct());

            var productUseCase = new ProductUseCase(mockRepository.Object);

            mockRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesCalled = true);

            //Act
            var result = await productUseCase.Update(It.IsAny<Product>());

            //Assert
            result.Should().BeOfType<Product>();
            Assert.True(result != null);
            Assert.True(saveChangesCalled);
        }

        [Fact]
        public async Task ApplySoftDeleteToProduct_ShouldReturnProductEnabledFalse()
        {
            //Arrange
            var mockRepository = new Mock<IProductRepository>();
            var saveChangesCalled = false;

            mockRepository
                .Setup(repository => repository.GetById(It.IsAny<int>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            mockRepository
                .Setup(repository => repository.Update(It.IsAny<Product>()))
                .Returns(ProductsFixture.GetOneProduct());

            mockRepository
                .Setup(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()))
                .Callback(() => saveChangesCalled = true);

            var productUseCase = new ProductUseCase(mockRepository.Object);

            //Act
            var result = await productUseCase.SoftDelete(It.IsAny<int>());

            //Assert
            Assert.False(result.Enabled);
            Assert.True(saveChangesCalled);
            mockRepository.Verify(mock => mock.UnitOfWork.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}