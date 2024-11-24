using FluentValidation;

namespace Manual.Movement.Manager.Application.UseCases.CreateManualMovement
{
    public class CreateManualMovementCommandValidator : AbstractValidator<CreateManualMovementCommand>
    {
        public CreateManualMovementCommandValidator()
        {
            RuleFor(x => x.Month)
                .NotEmpty()
                .WithMessage("Month cannot be null or empty.")
                .MaximumLength(2)
                .WithMessage("Month cannot be more than 2 characters.")
                .Must(BeAValidMonth)
                .WithMessage("Month must be a number between 1 and 12.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year cannot be null or empty.")
                .Length(4)
                .WithMessage("Year must be exactly 4 characters.")
                .Must(BeANumeric)
                .WithMessage("Year must be numeric.");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId cannot be null or empty.")
                .Length(4)
                .WithMessage("ProductId must be exactly 4 characters.");

            RuleFor(x => x.CosifId)
                .NotEmpty()
                .WithMessage("CosifId cannot be null or empty.")
                .Length(11)
                .WithMessage("CosifId must be exactly 11 characters.");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value cannot be null or empty.")
                .MaximumLength(18)
                .WithMessage("Value cannot exceed 18 characters.")
                .Must(BeANumeric)
                .WithMessage("Value must be numeric.");
        }

        private bool BeAValidMonth(string month)
        {
            if (int.TryParse(month, out var number))
            {
                return number >= 1 && number <= 12;
            }
            return false;
        }

        private bool BeANumeric(string value)
        {
            return decimal.TryParse(value, out _); 
        }
    }
}
