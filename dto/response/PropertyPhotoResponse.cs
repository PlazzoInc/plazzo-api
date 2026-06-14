namespace plazzo_api.dto.response;
public class PropertyPhotoResponse
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public byte Order { get; set; }
        public string Caption { get; set; } = string.Empty;
    }