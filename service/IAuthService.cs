using plazzo_api.dto.request.users;

namespace plazzo_api.service;
public interface IAuthService
    {
        Task<string?> RegisterAsync(RegisterRequest request);
        Task<string?> LoginAsync(LoginRequest request);
    }