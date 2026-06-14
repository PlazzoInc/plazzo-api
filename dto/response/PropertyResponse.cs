using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class PropertyResponse
    {
        public int Id { get; set; }
        public int AgencyId { get; set; }
        public int CommercialId { get; set; }

        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal SurfaceArea { get; set; }

        public byte RoomCount { get; set; }
        public byte BedroomCount { get; set; }
        public byte BathroomCount { get; set; }
        public byte Floor { get; set; }

        public int ConstructionYear { get; set; }
        public char EnergyRating { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public PropertyAddressResponse? Address { get; set; }
        public List<PropertyPhotoResponse> Photos { get; set; } = new();
        public List<PropertyFeatureResponse> Features { get; set; } = new();
    }