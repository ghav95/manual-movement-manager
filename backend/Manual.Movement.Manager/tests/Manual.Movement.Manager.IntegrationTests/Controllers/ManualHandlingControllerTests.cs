using Manual.Movement.Manager.Application.Dto;
using Manual.Movement.Manager.Application.UseCases.CreateManualMovement;
using Manual.Movement.Manager.Application.UseCases.GetAllManualMovement;
using Manual.Movement.Manager.WebApi.Controllers;
using Manual.Movement.Manager.WebApi.Transport.CreateManualMovement;
using Manual.Movement.Manager.WebApi.Transport.GetAllManualMovement;
using MediatR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Manual.Movement.Manager.IntegrationTests.Controllers
{
    [TestClass]
    public class ManualHandlingControllerTests
    {
        private Mock<IMediator> _mediatorMock;
        private ManualHandlingController _controller;

        [TestInitialize]
        public void Setup()
        {
            _mediatorMock = new Mock<IMediator>();

            // Initialize the controller
            _controller = new ManualHandlingController(_mediatorMock.Object);
        }

        [TestMethod]
        public async Task Post_Should_Return_Ok_When_Command_Is_Successful()
        {
            // Arrange
            var request = new CreateManualMovementRequest
            {
                Month = "12",
                Year = "2024",
                ProductId = "P001",
                CosifId = "C123",
                Description = "Test Movement",
                Value = "100.50"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateManualMovementCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CreateManualMovementOutput());

            // Act
            var result = await _controller.Post(request, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            Assert.IsInstanceOfType(result, typeof(OkResult));
        }

        [TestMethod]
        public async Task Post_Should_Return_InternalServerError_When_Command_Fails()
        {
            // Arrange
            var request = new CreateManualMovementRequest
            {
                Month = "12",
                Year = "2024",
                ProductId = "P001",
                CosifId = "C123",
                Description = "Test Movement",
                Value = "100.50"
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateManualMovementCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((CreateManualMovementOutput)null);

            // Act
            var result = await _controller.Post(request, CancellationToken.None)
                .ConfigureAwait(false); 

            // Assert
            var contentResult = result as NegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError, contentResult.StatusCode);
        }

        [TestMethod]
        public async Task Get_Should_Return_Ok_With_Data()
        {
            // Arrange
            var response = new GetAllManualMovementOutput(new List<ManualHandlingDto>
            {
                new ManualHandlingDto
                (
                    month: "12",
                    year: "2024",
                    launchNumber: "4",
                    productId: "P001",
                    cosifId: "C123",
                    description: "Test Movement",
                    date: DateTime.UtcNow.ToString(),
                    userId: "user1",
                    value: "100.50"
                )
            });

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllManualMovementCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(response);

            // Act
            var result = await _controller.Get(CancellationToken.None)
                .ConfigureAwait(false); 

            // Assert
            var okResult = result as OkNegotiatedContentResult<GetAllManualMovementResponse>;
            Assert.IsNotNull(okResult);
            Assert.AreEqual(1, okResult.Content.ManualHandlings.Count());
        }

        [TestMethod]
        public async Task Get_Should_Return_InternalServerError_When_Command_Fails()
        {
            // Arrange
            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllManualMovementCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((GetAllManualMovementOutput)null);

            // Act
            var result = await _controller.Get(CancellationToken.None)
                .ConfigureAwait(false); 

            // Assert
            var contentResult = result as NegotiatedContentResult<string>;
            Assert.IsNotNull(contentResult);
            Assert.AreEqual(HttpStatusCode.InternalServerError, contentResult.StatusCode);
        }
    }
}
