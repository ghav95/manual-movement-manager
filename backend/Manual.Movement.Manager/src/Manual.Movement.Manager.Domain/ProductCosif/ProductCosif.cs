using System;

namespace Manual.Movement.Manager.Domain.ProductCosif
{
    public class ProductCosif
    {
        public ProductCosif(
            string cosifId, 
            string classificationId, 
            char status)
        {
            CosifId = cosifId ?? throw new ArgumentNullException(nameof(cosifId));
            ClassificationId = classificationId ?? throw new ArgumentNullException(nameof(classificationId));
            Status = status;
        }

        public string Id { get; }
        public string CosifId { get; private set; } 
        public string ClassificationId { get; private set; }
        public char Status { get; private set; } 
    }
}
