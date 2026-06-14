namespace plazzo_api.entity;
public class PropertyFeature
    {
        public int Id { get; set; }

        public int PropertyId { get; set; }
        public Property? Property { get; set; }

        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }