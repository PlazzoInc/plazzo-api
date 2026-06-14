using plazzo_api.dto.request.analytics;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class AIPredictionService : IAIPredictionService
    {
        private readonly IAIPredictionRepository _repository;

        public AIPredictionService(IAIPredictionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AIPredictionResponse>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<AIPredictionResponse>> GetByPropertyIdAsync(int propertyId)
        {
            var items = await _repository.GetByPropertyIdAsync(propertyId);
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<AIPredictionResponse>> GetByTypeAsync(PredictionType type)
        {
            var items = await _repository.GetByTypeAsync(type);
            return items.Select(ToResponse).ToList();
        }

        public async Task<AIPredictionResponse> CreateAsync(CreateAIPredictionRequest request)
        {
            var prediction = new AIPrediction
            {
                PropertyId = request.PropertyId,
                Type = request.Type,
                Value = request.Value,
                Confidence = request.Confidence,
                ModelVersion = request.ModelVersion
            };

            var created = await _repository.CreateAsync(prediction);
            return ToResponse(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static AIPredictionResponse ToResponse(AIPrediction p) => new()
        {
            Id = p.Id,
            PropertyId = p.PropertyId,
            Type = p.Type,
            Value = p.Value,
            Confidence = p.Confidence,
            ModelVersion = p.ModelVersion,
            CreatedAt = p.CreatedAt
        };
    }