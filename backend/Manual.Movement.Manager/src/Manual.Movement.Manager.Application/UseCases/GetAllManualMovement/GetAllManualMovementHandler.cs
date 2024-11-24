using Manual.Movement.Manager.Application.Mapping;
using Manual.Movement.Manager.Domain.ManualHandling;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Application.UseCases.GetAllManualMovement
{
    public class GetAllManualMovementHandler : IRequestHandler<GetAllManualMovementCommand, GetAllManualMovementOutput>
    {
        private readonly IManualHandlingRepository _manualHandlingRepository;

        public GetAllManualMovementHandler(IManualHandlingRepository manualHandlingRepository)
            => _manualHandlingRepository = manualHandlingRepository;
        

        public async Task<GetAllManualMovementOutput> Handle(
            GetAllManualMovementCommand request, 
            CancellationToken cancellationToken)
        {
            var manualHandlingRepository = await _manualHandlingRepository.GetAllAsync(cancellationToken)
                .ConfigureAwait(false);

            var dto = manualHandlingRepository.ToDto();

            var output = new GetAllManualMovementOutput(dto);

            return output;
        }
    }
}
