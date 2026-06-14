using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class AIPredictionResponse
    {
        public int Id { get; set; }
        public int? PropertyId { get; set; }
        public PredictionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal Confidence { get; set; }
        public string ModelVersion { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }