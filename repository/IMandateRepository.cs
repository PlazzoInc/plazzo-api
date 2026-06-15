using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IMandateRepository
    {
        Task<List<Mandate>> GetAllAsync();
        Task<List<Mandate>> GetByCommercialIdAsync(int commercialId);
        Task<Mandate?> GetByIdAsync(int id);
        Task<Mandate> CreateAsync(Mandate mandate);
        Task<Mandate?> UpdateAsync(Mandate mandate);
        Task<bool> DeleteAsync(int id);
    }