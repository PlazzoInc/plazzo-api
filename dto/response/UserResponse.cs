using plazzo_api.entity;

namespace plazzo_api.dto.response;
public class UserResponse
    {
        public int Id { get; set; }
        public int? AgencyId { get; set; }
        public UserRole Role { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }