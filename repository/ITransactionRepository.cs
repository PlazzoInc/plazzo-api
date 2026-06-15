using plazzo_api.entity;

namespace plazzo_api.repository;
public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync();
        Task<List<Transaction>> GetByCommercialAsync(int commercialId);
        Task<Transaction?> GetByIdAsync(int id);
        Task<Transaction> CreateAsync(Transaction transaction);
        Task<Transaction?> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(int id);
    }