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
                .WithMessage("Month cannot be more than 2 characters.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year cannot be null or empty.")
                .Length(4)
                .WithMessage("Year must be exactly 4 characters.");

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

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be null or empty.");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value cannot be null or empty.")
                .MaximumLength(18)
                .WithMessage("Value cannot exceed 18 characters.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
