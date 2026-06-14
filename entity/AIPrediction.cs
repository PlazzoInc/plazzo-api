namespace plazzo_api.entity;
public enum PredictionType
    {
        EstimatedPrice,
        SaleDelay,
        AttractivenessScore,
        InterestZone
    }

    public class AIPrediction : BaseEntity
    {
        public int? PropertyId { get; set; }
        public Property? Property { get; set; }

        public PredictionType Type { get; set; }
        public decimal Value { get; set; }
        public decimal Confidence { get; set; } 
        public string ModelVersion { get; set; } = string.Empty;
    }