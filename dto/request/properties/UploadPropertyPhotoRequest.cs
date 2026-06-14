namespace plazzo_api.dto.request.properties;
public class UploadPropertyPhotoRequest
    {
        public IFormFile File { get; set; } = null!;
        public byte Order { get; set; } = 0;
        public string Caption { get; set; } = string.Empty;
    }