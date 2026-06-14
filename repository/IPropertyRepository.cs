using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IPropertyRepository
    {
        Task<List<Property>> GetAllAsync();
        Task<Property?> GetByIdAsync(int id);
        Task<Property> CreateAsync(Property property);
        Task<Property?> UpdateAsync(Property property);
        Task<bool> DeleteAsync(int id);
    }