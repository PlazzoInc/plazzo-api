namespace plazzo_api.entity;
public enum MandateType
    {
        Exclusive,
        Simple
    }

    public enum MandateStatus
    {
        Active,
        Expired,
        Cancelled,
        Completed
    }

    public class Mandate : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int ClientId { get; set; }
        public User? Client { get; set; }

        public int CommercialId { get; set; }
        public User? Commercial { get; set; }

        public MandateType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeePercentage { get; set; }
        public MandateStatus Status { get; set; } = MandateStatus.Active;
    }