using Manual.Movement.Manager.Application.UseCases.GetAllManualMovement;
using Manual.Movement.Manager.Domain.ManualHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.UnitTests.UseCases.GetAllManualMovement
{
    [TestClass]
    public class GetAllManualMovementHandlerTests
    {
        private Mock<IManualHandlingRepository> _manualHandlingRepositoryMock;
        private GetAllManualMovementHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _manualHandlingRepositoryMock = new Mock<IManualHandlingRepository>();
            _handler = new GetAllManualMovementHandler(_manualHandlingRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Call_Repository_And_Return_Valid_Output()
        {
            // Arrange
            var mockManualHandlingData = new List<ManualHandling>
            {
                new ManualHandling(
                    month: 12,
                    year: 2024,
                    launchNumber: 1,
                    productId: "P123",
                    cosifId: "C4567890123",
                    description: "Test Movement",
                    date: DateTime.Now,
                    userId: "user1",
                    value: 100.50m
                ),
                new ManualHandling(
                    month: 11,
                    year: 2024,
                    launchNumber: 2,
                    productId: "P124",
                    cosifId: "C9876543210",
                    description: "Another Test Movement",
                    date: DateTime.Now,
                    userId: "user2",
                    value: 200.75m
                )
            };

            _manualHandlingRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockManualHandlingData);

            var command = new GetAllManualMovementCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _manualHandlingRepositoryMock.Verify(repo => repo.GetAllAsync(
                It.IsAny<CancellationToken>()), 
                Times.Once);

            
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(GetAllManualMovementOutput));
            Assert.AreEqual(mockManualHandlingData.Count, result.ManualHandlings.Count());
        }

        [TestMethod]
        public async Task Handle_Should_Return_Empty_Output_When_Repository_Returns_Empty_List()
        {
            // Arrange
            _manualHandlingRepositoryMock
                .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ManualHandling>());

            var command = new GetAllManualMovementCommand();

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _manualHandlingRepositoryMock.Verify(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(GetAllManualMovementOutput));
            Assert.AreEqual(0, result.ManualHandlings.Count());
        }
    }
}
