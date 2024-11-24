namespace Manual.Movement.Manager.Application.Dto
{
    public class ProductCosifDto
    {
        public ProductCosifDto(string productId, string cosifId, string classificationId, string status)
        {
            ProductId = productId;
            CosifId = cosifId;
            ClassificationId = classificationId;
            Status = status;
        }

        public string ProductId { get; set; }
        public string CosifId { get; set; }
        public string ClassificationId { get; set; }
        public string Status { get; set; }
    }
}
