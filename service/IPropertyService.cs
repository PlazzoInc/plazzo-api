using plazzo_api.dto.request.properties;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IPropertyService
    {
        Task<List<PropertyResponse>> GetAllAsync();
        Task<PropertyResponse?> GetByIdAsync(int id);
        Task<PropertyResponse> CreateAsync(CreatePropertyRequest request, int currentUserId, int? currentUserAgencyId, bool isAdmin);
        Task<PropertyResponse?> UpdateAsync(int id, UpdatePropertyRequest request);
        Task<bool> DeleteAsync(int id);
    }