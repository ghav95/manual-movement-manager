using Manual.Movement.Manager.Application.Dto;
using Manual.Movement.Manager.Application.UseCases.GetAllProduct;
using System.Collections.Generic;

namespace Manual.Movement.Manager.WebApi.Transport.GetAllProduct
{
    public class GetAllProductResponse
    {
        public IEnumerable<ProductDto> Products { get; set; }
    }

    public static partial class OutputExtensions
    {
        public static GetAllProductResponse MapToResponse(this GetAllProductOutput output)
        {
            return new GetAllProductResponse
            {
                Products = output.ProductDtos
            };            
        }
    }
}