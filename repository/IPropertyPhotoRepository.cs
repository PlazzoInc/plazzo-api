using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IPropertyPhotoRepository
    {
        Task<PropertyPhoto?> GetByIdAsync(int id);
        Task<PropertyPhoto> CreateAsync(PropertyPhoto photo);
        Task<bool> DeleteAsync(int id);
        Task<bool> PropertyExistsAsync(int propertyId);
    }