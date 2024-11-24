using Manual.Movement.Manager.Application.Dto;
using Manual.Movement.Manager.Domain.Product;
using Manual.Movement.Manager.Domain.ProductCosif;
using System.Collections.Generic;
using System.Linq;

namespace Manual.Movement.Manager.Application.Mapping
{
    public static class ProductMapper
    {
        public static IEnumerable<ProductDto> ToDto(this IEnumerable<Product> product)
        {
            return product.Select(ToDto).ToList();
        }

        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto(
                product.Id,
                product.Description,
                product.Status,
                product.ProductCosifs.Select(ToDto).ToList()
                );
        }

        public static ProductCosifDto ToDto(this ProductCosif productCosif)
        {
            return new ProductCosifDto(
                productCosif.ProductId,
                productCosif.CosifId,
                productCosif.ClassificationId,
                productCosif.Status);
        }
    }
}
