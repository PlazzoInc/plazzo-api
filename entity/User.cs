namespace plazzo_api.entity;

    public enum UserRole
        {
            Admin,
            Comercial,
            Client
        }
    public class User : BaseEntity
    {

        public int? AgencyId { get; set; }
        public Agency? Agency { get; set; }

        public UserRole Role { get; set; } = UserRole.Client;

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
