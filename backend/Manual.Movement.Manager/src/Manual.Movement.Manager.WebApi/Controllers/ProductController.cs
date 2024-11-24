using Manual.Movement.Manager.Application.UseCases.GetAllProduct;
using Manual.Movement.Manager.WebApi.Transport.GetAllProduct;
using MediatR;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Manual.Movement.Manager.WebApi.Controllers
{
    /// <summary>
    /// Handles operations related to products.
    /// </summary>
    [RoutePrefix("api/v1/products")]
    public class ProductController : ApiController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
            => _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <param name="cancellationToken">Token for cancelling the operation.</param>
        /// <returns>A list of products or an error response.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get(
            CancellationToken cancellationToken = default)
        {
            var command = new GetAllProductCommand();

            var output = await _mediator.Send(command, cancellationToken)
                .ConfigureAwait(false);

            if (output != null) return Ok(output.MapToResponse());

            return Content(HttpStatusCode.BadRequest, "Failed to retrieve products.");
        }
    }
}
