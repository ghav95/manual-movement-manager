using Manual.Movement.Manager.Application.UseCases.CreateManualMovement;
using Manual.Movement.Manager.WebApi.Transport.CreateManualMovement;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi.Controllers
{
    /// <summary>
    /// Handles operations related to manual movements.
    /// </summary>
    [RoutePrefix("api/v1/movement")]
    public class ManualHandlingController : ApiController
    {
        private readonly IMediator _mediator;

        public ManualHandlingController(IMediator mediator) 
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post(
            [FromBody] CreateManualMovementRequest request,
            CancellationToken cancellationToken = default)
        {
            var command = new CreateManualMovementCommand(
                request.Month,
                request.Year,
                request.ProductId,
                request.CosifId,
                request.Description,
                request.UserId);

            var output = await _mediator.Send(command, cancellationToken)
                .ConfigureAwait(false);
                        
            if (output != null) return Ok();
            return InternalServerError();
        }
    }
}
