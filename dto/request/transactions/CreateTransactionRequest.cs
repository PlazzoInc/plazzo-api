namespace plazzo_api.dto.request.transactions;
public class CreateTransactionRequest
    {
        public int PropertyId { get; set; }
        public int OfferId { get; set; }
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public decimal FinalPrice { get; set; }
        public DateTime? CompromiseDate { get; set; }
        public DateTime? DeedDate { get; set; }
        public string Notary { get; set; } = string.Empty;
    }