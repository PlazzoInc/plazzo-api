using plazzo_api.entity;

namespace plazzo_api.dto.request.offers;
public class UpdateOfferRequest
    {
        public OfferStatus Status { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; } = string.Empty;
    }