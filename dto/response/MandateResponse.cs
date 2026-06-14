using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class MandateResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ClientId { get; set; }
        public int CommercialId { get; set; }
        public MandateType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeePercentage { get; set; }
        public MandateStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }