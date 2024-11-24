using System;

namespace Manual.Movement.Manager.Domain.ProductCosif
{
    public class ProductCosif
    {
        public ProductCosif(){}

        public ProductCosif(
            string productId,
            string cosifId, 
            string classificationId, 
            string status)
        {
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            CosifId = cosifId ?? throw new ArgumentNullException(nameof(cosifId));
            ClassificationId = classificationId ?? throw new ArgumentNullException(nameof(classificationId));
            Status = status ?? throw new ArgumentNullException(nameof(status));
        }

        public string ProductId { get; private set; }
        public string CosifId { get; private set; } 
        public string ClassificationId { get; private set; }
        public string Status { get; private set; } 
    }
}
