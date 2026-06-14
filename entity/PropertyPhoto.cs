namespace plazzo_api.entity;
public class PropertyPhoto : BaseEntity
    {
        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public string Url { get; set; } = string.Empty;
        public byte Order { get; set; }
        public string Caption { get; set; } = string.Empty;
    }