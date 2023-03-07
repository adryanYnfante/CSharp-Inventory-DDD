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
    public class ProductControllerTest
    {
        private Mock<IProductUseCase> mockUseCase;
        private Mock<ILogger<ProductController>> mockLogger;

        [Fact]
        public async Task GetAllProducts_OnSuccess_ValidateResult()
        {
            //Arrange
            var expectedProductId = 1;
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.GetAll())
                .ReturnsAsync(ProductsFixture.GetProductsTest());

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.GetAllProducts();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Product>>(okResult.Value);
            var product = returnValue.FirstOrDefault();
            Assert.Equal(expectedProductId, product.Id);
        }

        [Fact]
        public async Task GetProductById_OnSuccess_Returns200()
        {
            //Arrange
            var productId = 1;
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.GetById(It.IsAny<int>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.GetProductById(It.IsAny<int>());

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var product = Assert.IsType<Product>(okResult.Value);
            Assert.Equal(productId, product.Id);
        }

        [Fact]
        public async Task GetProductById_OnNotFound_Return404()
        {
            //Arrange
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.GetById(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.GetProductById(It.IsAny<int>());

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task CreateProduct_OnSuccess_ReturnsCreated201()
        {
            //Arrange
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.Add(It.IsAny<Product>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct);

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.CreateProduct(It.IsAny<Product>());

            //Assert
            result.Should().BeOfType<CreatedResult>();
            var objectResult = (CreatedResult)result;
            objectResult.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task UpdateProduct_OnSuccess_ReturnsOk()
        {
            //Arrange
            var productId = 1;
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.Update(It.IsAny<Product>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.UpdateProduct(productId, ProductsFixture.GetOneProduct());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task DisableProductById_OnSuccess_Returns200()
        {
            //Arrange
            var productId = 1;
            mockUseCase = new Mock<IProductUseCase>();
            mockLogger = new Mock<ILogger<ProductController>>();

            mockUseCase
                .Setup(useCase => useCase.SoftDelete(It.IsAny<int>()))
                .ReturnsAsync(ProductsFixture.GetOneProduct());

            var productController = new ProductController(mockUseCase.Object, mockLogger.Object);

            //Act
            var result = await productController.DisableProductById(productId);

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }
    }
}