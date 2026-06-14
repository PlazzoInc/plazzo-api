using plazzo_api.entity;

namespace plazzo_api.dto.request.properties;
public class UpdatePropertyRequest
    {
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

        public CreatePropertyAddressRequest Address { get; set; } = new();
        public List<CreatePropertyFeatureRequest> Features { get; set; } = new();
    }