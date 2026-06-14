namespace plazzo_api.entity;
public class PriceHistory : BaseEntity
    {
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public decimal AveragePricePerM2 { get; set; }
        public int TransactionCount { get; set; }
        public string Period { get; set; } = string.Empty; // format: YYYY-MM
        public string Source { get; set; } = string.Empty;
    }