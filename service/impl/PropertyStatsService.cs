using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class PropertyStatsService : IPropertyStatsService
    {
        private readonly IPropertyStatsRepository _repository;

        public PropertyStatsService(IPropertyStatsRepository repository)
        {
            _repository = repository;
        }

        public async Task<PropertyStatsResponse?> GetByPropertyIdAsync(int propertyId)
        {
            var stats = await _repository.GetByPropertyIdAsync(propertyId);
            return stats is null ? null : ToResponse(stats);
        }

        public async Task<List<PropertyStatsResponse>> GetTopByViewsAsync(int count = 10)
        {
            var items = await _repository.GetTopByViewsAsync(count);
            return items.Select(ToResponse).ToList();
        }

        public async Task<List<PropertyStatsResponse>> GetTopByOffersAsync(int count = 10)
        {
            var items = await _repository.GetTopByOffersAsync(count);
            return items.Select(ToResponse).ToList();
        }

        public async Task<PropertyStatsResponse?> IncrementViewAsync(int propertyId)
        {
            var stats = await _repository.IncrementViewAsync(propertyId);
            return stats is null ? null : ToResponse(stats);
        }

        public async Task<PropertyStatsResponse> GetOrCreateAsync(int propertyId)
        {
            var existing = await _repository.GetByPropertyIdAsync(propertyId);
            if (existing is not null) return ToResponse(existing);

            var created = await _repository.CreateAsync(new PropertyStats
            {
                PropertyId = propertyId,
                UpdatedAt = DateTime.UtcNow
            });

            return ToResponse(created);
        }

        private static PropertyStatsResponse ToResponse(PropertyStats s) => new()
        {
            Id = s.Id,
            PropertyId = s.PropertyId,
            ViewCount = s.ViewCount,
            FavoriteCount = s.FavoriteCount,
            VisitCount = s.VisitCount,
            OfferCount = s.OfferCount,
            DaysOnMarket = s.DaysOnMarket,
            UpdatedAt = s.UpdatedAt
        };
    }