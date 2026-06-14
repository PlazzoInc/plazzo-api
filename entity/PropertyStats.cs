namespace plazzo_api.entity;
public class PropertyStats
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public int ViewCount { get; set; } = 0;
        public int FavoriteCount { get; set; } = 0;
        public int VisitCount { get; set; } = 0;
        public int OfferCount { get; set; } = 0;
        public int DaysOnMarket { get; set; } = 0;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }