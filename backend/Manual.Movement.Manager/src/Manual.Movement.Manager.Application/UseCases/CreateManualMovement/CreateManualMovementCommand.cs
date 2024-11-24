using MediatR;

namespace Manual.Movement.Manager.Application.UseCases.CreateManualMovement
{
    public class CreateManualMovementCommand : IRequest<CreateManualMovementOutput>
    {
        public CreateManualMovementCommand(string month, string year, string productId, string cosifId, string description, string value, string userId = "TESTE")
        {
            Month = month;
            Year = year;
            ProductId = productId;
            CosifId = cosifId;
            Description = description;
            Value = value;
            UserId = userId;
        }

        public string Month { get; set; }
        public string Year { get; set; }
        public string ProductId { get; set; }
        public string CosifId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string UserId { get; set; }
    }
}
