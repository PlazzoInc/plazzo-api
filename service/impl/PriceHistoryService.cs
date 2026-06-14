using plazzo_api.dto.request.analytics;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class PriceHistoryService : IPriceHistoryService
    {
        private readonly IPriceHistoryRepository _repository;

        public PriceHistoryService(IPriceHistoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PriceHistoryResponse>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<PriceHistoryResponse>> GetByCityAsync(string city)
        {
            var items = await _repository.GetByCityAsync(city);
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<PriceHistoryResponse>> GetByPostalCodeAsync(string postalCode)
        {
            var items = await _repository.GetByPostalCodeAsync(postalCode);
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<PriceHistoryResponse>> GetByPeriodAsync(string period)
        {
            var items = await _repository.GetByPeriodAsync(period);
            return items.Select(ToResponse).ToList();
        }

        public async Task<PriceHistoryResponse> CreateAsync(CreatePriceHistoryRequest request)
        {
            var item = new PriceHistory
            {
                City = request.City,
                PostalCode = request.PostalCode,
                PropertyType = request.PropertyType,
                AveragePricePerM2 = request.AveragePricePerM2,
                TransactionCount = request.TransactionCount,
                Period = request.Period,
                Source = request.Source
            };

            var created = await _repository.CreateAsync(item);
            return ToResponse(created);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static PriceHistoryResponse ToResponse(PriceHistory p) => new()
        {
            Id = p.Id,
            City = p.City,
            PostalCode = p.PostalCode,
            PropertyType = p.PropertyType,
            AveragePricePerM2 = p.AveragePricePerM2,
            TransactionCount = p.TransactionCount,
            Period = p.Period,
            Source = p.Source,
            CreatedAt = p.CreatedAt
        };
    }