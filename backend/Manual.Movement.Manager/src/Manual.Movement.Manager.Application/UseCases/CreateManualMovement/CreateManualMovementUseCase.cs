using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Application.UseCases.CreateManualMovement
{
    public class CreateManualMovementUseCase : IRequestHandler<CreateManualMovementCommand, CreateManualMovementOutput>
    {
        public Task<CreateManualMovementOutput> Handle(CreateManualMovementCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
