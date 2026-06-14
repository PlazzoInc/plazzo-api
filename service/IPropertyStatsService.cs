using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IPropertyStatsService
    {
        Task<PropertyStatsResponse?> GetByPropertyIdAsync(int propertyId);
        Task<List<PropertyStatsResponse>> GetTopByViewsAsync(int count = 10);
        Task<List<PropertyStatsResponse>> GetTopByOffersAsync(int count = 10);
        Task<PropertyStatsResponse?> IncrementViewAsync(int propertyId);
        Task<PropertyStatsResponse> GetOrCreateAsync(int propertyId);
    }