namespace plazzo_api.dto.response;
public class PropertyStatsResponse
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int ViewCount { get; set; }
        public int FavoriteCount { get; set; }
        public int VisitCount { get; set; }
        public int OfferCount { get; set; }
        public int DaysOnMarket { get; set; }
        public DateTime UpdatedAt { get; set; }
    }