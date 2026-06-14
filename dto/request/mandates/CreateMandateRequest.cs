using plazzo_api.entity;

namespace plazzo_api.dto.request.mandates;
public class CreateMandateRequest
    {
        public int PropertyId { get; set; }
        public int ClientId { get; set; }
        public int CommercialId { get; set; }
        public MandateType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeePercentage { get; set; }
    }