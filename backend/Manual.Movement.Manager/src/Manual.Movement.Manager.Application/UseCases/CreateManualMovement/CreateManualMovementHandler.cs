using Manual.Movement.Manager.Domain.ManualHandling;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Application.UseCases.CreateManualMovement
{
    public class CreateManualMovementHandler : IRequestHandler<CreateManualMovementCommand, CreateManualMovementOutput>
    {
        private readonly IManualHandlingRepository _manualHandlingRepository;

        public CreateManualMovementHandler(IManualHandlingRepository manualHandlingRepository) 
            => _manualHandlingRepository = manualHandlingRepository;

        public async Task<CreateManualMovementOutput> Handle(
            CreateManualMovementCommand request, 
            CancellationToken cancellationToken)
        {
            await _manualHandlingRepository.AddAsync(
                int.Parse(request.Month),
                int.Parse(request.Year),
                request.ProductId,
                request.CosifId,
                request.Description,
                decimal.Parse(request.Value),
                request.UserId)
                .ConfigureAwait(false);

            return new CreateManualMovementOutput();
        }
    }
}
