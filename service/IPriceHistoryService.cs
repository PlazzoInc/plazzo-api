using plazzo_api.dto.request.analytics;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IPriceHistoryService
    {
        Task<List<PriceHistoryResponse>> GetAllAsync();
        Task<List<PriceHistoryResponse>> GetByCityAsync(string city);
        Task<List<PriceHistoryResponse>> GetByPostalCodeAsync(string postalCode);
        Task<List<PriceHistoryResponse>> GetByPeriodAsync(string period);
        Task<PriceHistoryResponse> CreateAsync(CreatePriceHistoryRequest request);
        Task<bool> DeleteAsync(int id);
    }