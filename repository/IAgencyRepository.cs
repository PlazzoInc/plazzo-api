using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IAgencyRepository
    {
        Task<List<Agency>> GetAllAsync();
        Task<Agency?> GetByIdAsync(int id);
        Task<Agency> CreateAsync(Agency agency);
        Task<Agency?> UpdateAsync(Agency agency);
        Task<bool> DeleteAsync(int id);
    }