using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IPriceHistoryRepository
    {
        Task<List<PriceHistory>> GetAllAsync();
        Task<List<PriceHistory>> GetByCityAsync(string city);
        Task<List<PriceHistory>> GetByPostalCodeAsync(string postalCode);
        Task<List<PriceHistory>> GetByPeriodAsync(string period);
        Task<PriceHistory> CreateAsync(PriceHistory priceHistory);
        Task<bool> DeleteAsync(int id);
    }