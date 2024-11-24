using System.Collections.Generic;

namespace Manual.Movement.Manager.Application.Dto
{
    public class ProductDto
    {
        public ProductDto(string id, string description, string status, IEnumerable<ProductCosifDto> productCosifDto)
        {
            Id = id;
            Description = description;
            Status = status;
            ProductCosifs = productCosifDto;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public IEnumerable<ProductCosifDto> ProductCosifs { get; set; }

    }
}
