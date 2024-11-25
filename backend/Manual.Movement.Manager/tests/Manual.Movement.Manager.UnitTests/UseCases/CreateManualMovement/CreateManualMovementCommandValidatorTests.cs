using FluentValidation.TestHelper;
using Manual.Movement.Manager.Application.UseCases.CreateManualMovement;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.AutoFixture;

namespace Manual.Movement.Manager.UnitTests.UseCases.CreateManualMovement
{
    [TestClass]
    public class CreateManualMovementCommandValidatorTests
    {
        private Fixture _fixture;
        private CreateManualMovementCommandValidator _validator;

        [TestInitialize]
        public void Setup()
        {
            _validator = new CreateManualMovementCommandValidator();
            _fixture = new Fixture();
        }

        [TestMethod]
        public void Should_Have_Error_When_Month_Is_Empty()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Month, "")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Month)
                  .WithErrorMessage("Month cannot be null or empty.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Month_Exceeds_Max_Length()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Month, "123")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Month)
                  .WithErrorMessage("Month cannot be more than 2 characters.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Month_Is_Not_Between_1_And_12()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Month, "13")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Month)
                  .WithErrorMessage("Month must be a number between 1 and 12.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Year_Is_Empty()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Year, "")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Year)
                  .WithErrorMessage("Year cannot be null or empty.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Year_Is_Not_Exactly_Four_Characters()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Year, "12345")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Year)
                  .WithErrorMessage("Year must be exactly 4 characters.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Year_Is_Not_Numeric()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Year, "abcd")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Year)
                  .WithErrorMessage("Year must be numeric.");
        }

        [TestMethod]
        public void Should_Have_Error_When_Value_Is_Not_Numeric()
        {
            var command = _fixture.Build<CreateManualMovementCommand>()
                .With(c => c.Value, "abcd")
                .Create();

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Value)
                  .WithErrorMessage("Value must be numeric.");
        }

        [TestMethod]
        public void Should_Have_No_Errors_When_Command_Is_Valid()
        {

            var command = new CreateManualMovementCommand(
                month: "12",
                year: "2024",
                productId: "1234",
                cosifId: "12345678901",
                description: "Description",
                value: "100.50"
            );
            var result = _validator.TestValidate(command);

            Assert.IsTrue(result.IsValid);
        }
    }
}