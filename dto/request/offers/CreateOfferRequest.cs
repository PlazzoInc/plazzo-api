namespace plazzo_api.dto.request.offers;
public class CreateOfferRequest
    {
        public int PropertyId { get; set; }
        public int CommercialId { get; set; }
        public decimal Amount { get; set; }
        public string Message { get; set; } = string.Empty;
    }