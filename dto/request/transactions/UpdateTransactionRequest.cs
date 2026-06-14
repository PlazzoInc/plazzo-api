using plazzo_api.entity;

namespace plazzo_api.dto.request.transactions;
public class UpdateTransactionRequest
    {
        public decimal FinalPrice { get; set; }
        public DateTime? CompromiseDate { get; set; }
        public DateTime? DeedDate { get; set; }
        public string Notary { get; set; } = string.Empty;
        public TransactionStatus Status { get; set; }
    }