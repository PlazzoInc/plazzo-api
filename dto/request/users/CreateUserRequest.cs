using plazzo_api.entity;

namespace plazzo_api.dto.request.users;
public class CreateUserRequest
    {
        public int? AgencyId { get; set; }
        public UserRole Role { get; set; } = UserRole.Client;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }