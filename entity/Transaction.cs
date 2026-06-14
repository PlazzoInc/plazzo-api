namespace plazzo_api.entity;
public enum TransactionStatus
    {
        InProgress,
        CompromiseSigned,
        Completed,
        Cancelled
    }

    public class Transaction : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int OfferId { get; set; }
        public Offer? Offer { get; set; }

        public int SellerId { get; set; }
        public User? Seller { get; set; }

        public int BuyerId { get; set; }
        public User? Buyer { get; set; }

        public decimal FinalPrice { get; set; }
        public DateTime? CompromiseDate { get; set; }
        public DateTime? DeedDate { get; set; }
        public string Notary { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; } = TransactionStatus.InProgress;
    }