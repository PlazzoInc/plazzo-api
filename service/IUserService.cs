using plazzo_api.dto.request.users;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IUserService
    {
        Task<List<UserResponse>> GetAllAsync();
        Task<UserResponse?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(CreateUserRequest request);
        Task<UserResponse?> UpdateAsync(int id, UpdateUserRequest request);
        Task<bool> DeleteAsync(int id);
    }