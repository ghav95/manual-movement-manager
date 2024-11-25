using Manual.Movement.Manager.Application.UseCases.CreateManualMovement;
using Manual.Movement.Manager.Domain.ManualHandling;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.UnitTests.UseCases.CreateManualMovement
{
    [TestClass]
    public class CreateManualMovementHandlerTests
    {
        private Mock<IManualHandlingRepository> _manualHandlingRepositoryMock;
        private CreateManualMovementHandler _handler;

        [TestInitialize]
        public void Setup()
        {
            _manualHandlingRepositoryMock = new Mock<IManualHandlingRepository>();
            _handler = new CreateManualMovementHandler(_manualHandlingRepositoryMock.Object);
        }

        [TestMethod]
        public async Task Handle_Should_Call_Repository_With_Correct_Parameters()
        {
            // Arrange
            var command = new CreateManualMovementCommand(
                month: "12",
                year: "2024",
                productId: "1234",
                cosifId: "56789",
                description: "Test Description",
                value: "100.50",
                userId: "test_user"
            );
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None)
                .ConfigureAwait(false);

            // Assert
            _manualHandlingRepositoryMock.Verify(repo => repo.AddAsync(
                int.Parse(command.Month),
                int.Parse(command.Year),
                command.ProductId,
                command.CosifId,
                command.Description,
                decimal.Parse(command.Value),
                command.UserId,
                It.IsAny<CancellationToken>()),
                Times.Once);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CreateManualMovementOutput));
        }

        [TestMethod]
        public async Task Handle_Should_Throw_Exception_When_Repository_Fails()
        {
            // Arrange
            var command = new CreateManualMovementCommand(
                month: "12",
                year: "2024",
                productId: "1234",
                cosifId: "56789",
                description: "Test Description",
                value: "100.50",
                userId: "test_user"
            );

            _manualHandlingRepositoryMock
                .Setup(repo => repo.AddAsync(
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ))
                .ThrowsAsync(new System.Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<System.Exception>(async () =>
                await _handler.Handle(command, CancellationToken.None))
                .ConfigureAwait(false);

            _manualHandlingRepositoryMock.Verify(repo => repo.AddAsync(
                int.Parse(command.Month),
                int.Parse(command.Year),
                command.ProductId,
                command.CosifId,
                command.Description,
                decimal.Parse(command.Value),
                command.UserId,
                It.IsAny<CancellationToken>()
            ), Times.Once);
        }
    }
}
