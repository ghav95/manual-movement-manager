using Manual.Movement.Manager.Application.Mapping;
using Manual.Movement.Manager.Domain.Product;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Manual.Movement.Manager.Application.UseCases.GetAllProduct
{
    public class GetAllProductHandler : IRequestHandler<GetAllProductCommand, GetAllProductOutput>
    {
        private readonly IProductRepository _repository;

        public GetAllProductHandler(IProductRepository productRepository) 
            => _repository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));        

        public async Task<GetAllProductOutput> Handle(GetAllProductCommand request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync(cancellationToken);

            var dtos = products.ToDto();

            var output = new GetAllProductOutput(dtos);

            return output;
        }
    }
}