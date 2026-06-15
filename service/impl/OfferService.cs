using plazzo_api.dto.request.offers;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class OfferService : IOfferService
    {
        private readonly IOfferRepository _repository;
        private readonly IPropertyStatsRepository _statsRepository;

        public OfferService(IOfferRepository repository, IPropertyStatsRepository statsRepository)
        {
            _repository = repository;
            _statsRepository = statsRepository;
        }

        public async Task<List<OfferResponse>> GetAllAsync(int? commercialId = null)
        {
            var offers = commercialId.HasValue
                ? await _repository.GetByCommercialPropertyAsync(commercialId.Value)
                : await _repository.GetAllAsync();
            return offers.Select(ToResponse).ToList();
        }

        public async Task<OfferResponse?> GetByIdAsync(int id)
        {
            var offer = await _repository.GetByIdAsync(id);
            return offer is null ? null : ToResponse(offer);
        }

        public async Task<OfferResponse> CreateAsync(CreateOfferRequest request, int buyerId)
        {
            var offer = new Offer
            {
                PropertyId = request.PropertyId,
                BuyerId = buyerId,
                CommercialId = request.CommercialId,
                Amount = request.Amount,
                Message = request.Message,
                OfferDate = DateTime.UtcNow,
                Status = OfferStatus.Pending
            };

            var created = await _repository.CreateAsync(offer);

            try
            {
                var stats = await _statsRepository.GetByPropertyIdAsync(request.PropertyId);
                if (stats is null)
                {
                    await _statsRepository.CreateAsync(new PropertyStats
                    {
                        PropertyId = request.PropertyId,
                        OfferCount = 1
                    });
                }
                else
                {
                    stats.OfferCount++;
                    stats.UpdatedAt = DateTime.UtcNow;
                    await _statsRepository.UpdateAsync(stats);
                }
            }
            catch
            {
                // offer count tracking must not block offer creation
            }

            return ToResponse(created);
        }

        public async Task<OfferResponse?> UpdateAsync(int id, UpdateOfferRequest request)
        {
            var offer = new Offer
            {
                Id = id,
                Status = request.Status,
                Amount = request.Amount,
                Message = request.Message
            };

            var updated = await _repository.UpdateAsync(offer);
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static OfferResponse ToResponse(Offer o) => new()
        {
            Id = o.Id,
            PropertyId = o.PropertyId,
            BuyerId = o.BuyerId,
            CommercialId = o.CommercialId,
            Amount = o.Amount,
            OfferDate = o.OfferDate,
            Status = o.Status,
            Message = o.Message,
            CreatedAt = o.CreatedAt
        };
    }