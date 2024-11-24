namespace Manual.Movement.Manager.Application.Dto
{
    public class ManualHandlingDto
    {
        public ManualHandlingDto(string month, string year, string launchNumber, string productId, string cosifId, string description, string date, string userId, string value)
        {
            Month = month;
            Year = year;
            LaunchNumber = launchNumber;
            ProductId = productId;
            CosifId = cosifId;
            Description = description;
            Date = date;
            UserId = userId;
            Value = value;
        }

        public string Month { get; set; }
        public string Year { get; set; }
        public string LaunchNumber { get; set; }
        public string ProductId { get; set; }
        public string CosifId { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; }
        public string Value { get; set; }
    }
}
