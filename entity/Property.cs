namespace plazzo_api.entity;    
    public class Property : BaseEntity
    
    {
        public int Agency_Id { get; set; }
        public int User_Id { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Deed { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Surface { get; set; }
        public byte Rooms { get; set; }
        public byte Bathrooms { get; set; }
        public byte Bedrooms { get; set; }
        public byte Floor { get; set; }
        public DateTime Construction_Date { get; set; }
        public Char DPE { get; set; }
        public string updated_at { get; set; }

    }
