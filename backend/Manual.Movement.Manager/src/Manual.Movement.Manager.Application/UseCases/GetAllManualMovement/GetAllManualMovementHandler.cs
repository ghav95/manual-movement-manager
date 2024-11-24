using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Application.UseCases.GetAllManualMovement
{
    public class GetAllManualMovementHandler : IRequestHandler<GetAllManualMovementCommand, GetAllManualMovementOutput>
    {
        public async Task<GetAllManualMovementOutput> Handle(GetAllManualMovementCommand request, CancellationToken cancellationToken)
        {
            await Task.Delay(100);
            return new GetAllManualMovementOutput();
        }
    }
}
