using Manual.Movement.Manager.Application.Dto;
using System.Collections.Generic;

namespace Manual.Movement.Manager.Application.UseCases.GetAllProduct
{
    public class GetAllProductOutput
    {
        public GetAllProductOutput(IEnumerable<ProductDto> productDtos)
        {
            ProductDtos = productDtos;
        }

        public IEnumerable<ProductDto> ProductDtos { get; set; }
    }
}
