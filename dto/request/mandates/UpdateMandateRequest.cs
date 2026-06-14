using plazzo_api.entity;

namespace plazzo_api.dto.request.mandates;
public class UpdateMandateRequest
    {
        public MandateType Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal FeePercentage { get; set; }
        public MandateStatus Status { get; set; }
    }