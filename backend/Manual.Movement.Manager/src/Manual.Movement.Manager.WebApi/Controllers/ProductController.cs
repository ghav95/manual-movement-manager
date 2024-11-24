using Manual.Movement.Manager.Application.UseCases.GetAllProduct;
using Manual.Movement.Manager.WebApi.Transport.GetAllProduct;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi.Controllers
{
    public class ProductController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator) 
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


        public async Task<IHttpActionResult> Get(CancellationToken cancellationToken = default)
        {
            var command = new GetAllProductCommand();

            var output = await _mediator.Send(command, cancellationToken)
                .ConfigureAwait(false);

            if(output != null) return Ok(output.MapToResponse());
            return BadRequest();
        }
    }
}