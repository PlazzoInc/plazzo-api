namespace plazzo_api.entity;
public enum PropertyType
    {
        Apartment,
        House,
        Office,
        CommercialSpace
    }

    public enum PropertyStatus
    {
        Available,
        UnderOffer,
        Sold,
        Withdrawn
    }

    public class Property : BaseEntity
    {
        public int AgencyId { get; set; }
        public Agency? Agency { get; set; }

        public int CommercialId { get; set; }
        public User? Commercial { get; set; }

        public PropertyType Type { get; set; }
        public PropertyStatus Status { get; set; } = PropertyStatus.Available;

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

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public PropertyAddress? Address { get; set; }
        public ICollection<PropertyPhoto> Photos { get; set; } = new List<PropertyPhoto>();
        public ICollection<PropertyFeature> Features { get; set; } = new List<PropertyFeature>();
    }