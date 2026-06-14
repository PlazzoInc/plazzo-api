using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class TransactionResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int OfferId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime? CompromiseDate { get; set; }
        public DateTime? DeedDate { get; set; }
        public string Notary { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }