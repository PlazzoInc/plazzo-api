using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class OfferResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int BuyerId { get; set; }
        public int CommercialId { get; set; }
        public decimal Amount { get; set; }
        public DateTime OfferDate { get; set; }
        public OfferStatus Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }