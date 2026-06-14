using plazzo_api.dto.request.analytics;
using plazzo_api.dto.response;
using plazzo_api.entity;

namespace plazzo_api.service;
public interface IAIPredictionService
    {
        Task<List<AIPredictionResponse>> GetAllAsync();
        Task<List<AIPredictionResponse>> GetByPropertyIdAsync(int propertyId);
        Task<List<AIPredictionResponse>> GetByTypeAsync(PredictionType type);
        Task<AIPredictionResponse> CreateAsync(CreateAIPredictionRequest request);
        Task<bool> DeleteAsync(int id);
    }