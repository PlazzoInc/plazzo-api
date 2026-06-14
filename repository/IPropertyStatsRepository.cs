using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IPropertyStatsRepository
    {
        Task<PropertyStats?> GetByPropertyIdAsync(int propertyId);
        Task<List<PropertyStats>> GetTopByViewsAsync(int count);
        Task<List<PropertyStats>> GetTopByOffersAsync(int count);
        Task<PropertyStats> CreateAsync(PropertyStats stats);
        Task<PropertyStats?> IncrementViewAsync(int propertyId);
        Task<PropertyStats?> UpdateAsync(PropertyStats stats);
    }