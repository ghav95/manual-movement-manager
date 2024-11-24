namespace Manual.Movement.Manager.WebApi.Transport.CreateManualMovement
{
    public class CreateManualMovementRequest
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string ProductId { get; set; }
        public string CosifId { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string UserId { get; set; }
    }
}