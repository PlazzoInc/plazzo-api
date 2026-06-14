using plazzo_api.entity;

namespace plazzo_api.dto.request.analytics;
public class CreateAIPredictionRequest
    {
        public int? PropertyId { get; set; }
        public PredictionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal Confidence { get; set; }
        public string ModelVersion { get; set; } = string.Empty;
    }