using System;

namespace Manual.Movement.Manager.Domain.ManualHandling
{
    public class ManualHandling
    {

        public ManualHandling(){}

        public ManualHandling(
            int month, 
            int year, 
            long launchNumber, 
            string productId, 
            string cosifId, 
            string description, 
            DateTime date, 
            string userId, 
            decimal value)
        {
            Month = month;
            Year = year;
            LaunchNumber = launchNumber;
            ProductId = productId ?? throw new ArgumentNullException(nameof(productId));
            CosifId = cosifId ?? throw new ArgumentNullException(nameof(cosifId));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Date = date;
            UserId = userId ?? throw new ArgumentNullException(nameof(userId));
            Value = value;
        }

        public int Month { get; private set; }
        public int Year { get; private set; } 
        public long LaunchNumber { get; private set; }
        public string ProductId { get; private set; } 
        public string CosifId { get; private set; } 
        public string Description { get; private set; }
        public DateTime Date { get; private set; } 
        public string UserId { get; private set; } 
        public decimal Value { get; private set; }

        public virtual ProductCosif.ProductCosif ProductCosif { get; private set; }
    }
}
