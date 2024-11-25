using Manual.Movement.Manager.Application.UseCases.GetAllProduct;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Domain.ProductCosif;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.UnitTests.UseCases.GetAllProduct
{
    [TestClass]
    public class GetAllProductHandlerTests
    {
        private Mock<IProductRepository> _productRepositoryMock;
        private GetAllProductHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _handler = new GetAllProductHandler(_productRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Call_Repository_And_Return_Valid_Output()
        {
            // Arrange
            var mockProductData = new List<Product>
            {
                Product.CreateProduct(
                    id: "P001",
                    description: "Product 1",
                    status: "A",
                    productCosif: new List<ProductCosif>
                    {
                        new ProductCosif("P001", "C123456789", "CL001", "A"),
                        new ProductCosif("P001", "C987654321", "CL002", "I")
                    }
                ),
                Product.CreateProduct(
                    id: "P002",
                    description: "Product 2",
                    status: "I",
                    productCosif: new List<ProductCosif>
                    {
                        new ProductCosif("P002", "C123456789", "CL001", "A")
                    }
                )
            };

            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockProductData);

            var command = new GetAllProductCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(GetAllProductOutput));
            Assert.AreEqual(mockProductData.Count, result.ProductDtos.Count());

            // Validate the navigation properties

            var xpto = result.ProductDtos.ElementAt(0);
            Assert.AreEqual(2, result.ProductDtos.ElementAt(0).ProductCosifs.Count());
            Assert.AreEqual(1, result.ProductDtos.ElementAt(1).ProductCosifs.Count());
        }

        [TestMethod]
        public async Task Handle_Should_Return_Empty_Output_When_Repository_Returns_Empty_List()
        {
            // Arrange
            _productRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<Product>());

            var command = new GetAllProductCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(GetAllProductOutput));
            Assert.AreEqual(0, result.ProductDtos.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Constructor_Should_Throw_Exception_When_Repository_Is_Null()
        {
            // Arrange, Act, Assert
            new GetAllProductHandler(null);
        }
    }
}
