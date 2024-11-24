using FluentValidation;

namespace Manual.Movement.Manager.Application.UseCases.CreateManualMovement
{
    public class CreateManualMovementCommandValidator : AbstractValidator<CreateManualMovementCommand>
    {
        public CreateManualMovementCommandValidator()
        {
            RuleFor(x => x.Month)
                .NotEmpty()
                .WithMessage("Month cannot be null or empty.");

            RuleFor(x => x.Year)
                .NotEmpty()
                .WithMessage("Year cannot be null or empty.");

            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("ProductId cannot be null or empty.");

            RuleFor(x => x.CosifId)
                .NotEmpty()
                .WithMessage("CosifId cannot be null or empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description cannot be null or empty.");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value cannot be null or empty.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId cannot be null or empty.");
        }
    }
}
