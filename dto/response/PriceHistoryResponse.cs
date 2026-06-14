namespace plazzo_api.dto.response;
public class PriceHistoryResponse
    {
        public int Id { get; set; }
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string PropertyType { get; set; } = string.Empty;
        public decimal AveragePricePerM2 { get; set; }
        public int TransactionCount { get; set; }
        public string Period { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }