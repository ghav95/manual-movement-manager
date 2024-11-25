using Manual.Movement.Manager.Application.UseCases.GetAllProduct;
using Manual.Movement.Manager.WebApi.Controllers;
using Manual.Movement.Manager.WebApi.Transport.GetAllProduct;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Http;
using Manual.Movement.Manager.Application.Dto;
using Ploeh.AutoFixture;

namespace Manual.Movement.Manager.IntegrationTests.Controllers
{
    [TestClass]
    public class ProductControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private ProductController _controller;
        private Fixture _fixture;

        [TestInitialize]
        public void Setup()
        {            
            _mediatorMock = new Mock<IMediator>();                        
            _controller = new ProductController(_mediatorMock.Object);
            _fixture = new Fixture();
        }

        [TestMethod]
        public async Task Get_Should_Return_Ok_With_Data()
        {
            // Arrange

            var ProductDto = _fixture.CreateMany<ProductDto>(2);
            
            var mockResponse = new GetAllProductOutput(ProductDto);

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse);

            // Act
            var result = await _controller.Get(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            var okResult = result as OkNegotiatedContentResult<GetAllProductResponse>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(2, okResult.Content.Products.Count());
            Assert.AreEqual("P001", okResult.Content.Products.ElementAt(0).Id);
            Assert.AreEqual("Product 1", okResult.Content.Products.ElementAt(0).Description);
        }

        [TestMethod]
        public async Task Get_Should_Return_InternalServerError_When_Mediator_Returns_Null()
        {
            // Arrange
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllProductCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetAllProductOutput)null);

            // Act
            var result = await _controller.Get(CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            var contentResult = result as NegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError, contentResult.StatusCode);
            Assert.AreEqual("Failed to retrieve products.", contentResult.Content);
        }
    }
}
