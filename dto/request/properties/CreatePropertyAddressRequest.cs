namespace plazzo_api.dto.request.properties;
public class CreatePropertyAddressRequest
    {
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }