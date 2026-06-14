using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<bool> EmailExistsAsync(string email);
    }