using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IAIPredictionRepository
    {
        Task<List<AIPrediction>> GetAllAsync();
        Task<List<AIPrediction>> GetByPropertyIdAsync(int propertyId);
        Task<List<AIPrediction>> GetByTypeAsync(PredictionType type);
        Task<AIPrediction> CreateAsync(AIPrediction prediction);
        Task<bool> DeleteAsync(int id);
    }