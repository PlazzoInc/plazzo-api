using plazzo_api.dto.request.users;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;


namespace plazzo_api.service.impl;
public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<UserResponse>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return users.Select(ToResponse).ToList();
        }

        public async Task<UserResponse?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);
            return user is null ? null : ToResponse(user);
        }

        public async Task<UserResponse> CreateAsync(CreateUserRequest request)
        {
            var user = new User
            {
                AgencyId = request.AgencyId,
                Role = request.Role,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Phone = request.Phone,
                UpdatedAt = DateTime.UtcNow
            };

            var created = await _repository.CreateAsync(user);
            return ToResponse(created);
        }

        public async Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request)
        {
            var user = new User
            {
                Id = id,
                AgencyId = request.AgencyId,
                Role = request.Role,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone
            };

            var updated = await _repository.UpdateAsync(user);
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static UserResponse ToResponse(User user) => new()
        {
            Id = user.Id,
            AgencyId = user.AgencyId,
            Role = user.Role,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            Phone = user.Phone,
            CreatedAt = user.CreatedAt,
            UpdatedAt = user.UpdatedAt
        };
    }