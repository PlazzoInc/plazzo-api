namespace plazzo_api.entity;
public enum OfferStatus
    {
        Pending,
        Accepted,
        Refused,
        Cancelled
    }

    public class Offer : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int BuyerId { get; set; }
        public User? Buyer { get; set; }

        public int CommercialId { get; set; }
        public User? Commercial { get; set; }

        public decimal Amount { get; set; }
        public DateTime OfferDate { get; set; } = DateTime.UtcNow;
        public OfferStatus Status { get; set; } = OfferStatus.Pending;
        public string Message { get; set; } = string.Empty;
    }