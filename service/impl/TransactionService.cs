using plazzo_api.dto.request.transactions;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPriceHistoryRepository _priceHistoryRepository;

        public TransactionService(
            ITransactionRepository repository,
            IPropertyRepository propertyRepository,
            IPriceHistoryRepository priceHistoryRepository)
        {
            _repository = repository;
            _propertyRepository = propertyRepository;
            _priceHistoryRepository = priceHistoryRepository;
        }

        public async Task<List<TransactionResponse>> GetAllAsync(int? commercialId = null)
        {
            var transactions = commercialId.HasValue
                ? await _repository.GetByCommercialAsync(commercialId.Value)
                : await _repository.GetAllAsync();
            return transactions.Select(ToResponse).ToList();
        }

        public async Task<TransactionResponse?> GetByIdAsync(int id)
        {
            var transaction = await _repository.GetByIdAsync(id);
            return transaction is null ? null : ToResponse(transaction);
        }

        public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request)
        {
            var transaction = new Transaction
            {
                PropertyId = request.PropertyId,
                OfferId = request.OfferId,
                SellerId = request.SellerId,
                BuyerId = request.BuyerId,
                FinalPrice = request.FinalPrice,
                CompromiseDate = request.CompromiseDate,
                DeedDate = request.DeedDate,
                Notary = request.Notary,
                Status = TransactionStatus.InProgress
            };

            var created = await _repository.CreateAsync(transaction);
            return ToResponse(created);
        }

        public async Task<TransactionResponse?> UpdateAsync(int id, UpdateTransactionRequest request)
        {
            var transaction = new Transaction
            {
                Id = id,
                FinalPrice = request.FinalPrice,
                CompromiseDate = request.CompromiseDate,
                DeedDate = request.DeedDate,
                Notary = request.Notary,
                Status = request.Status
            };

            var updated = await _repository.UpdateAsync(transaction);
            if (updated is null) return null;

            if (updated.Status == TransactionStatus.Completed)
            {
                try
                {
                    await RecordPriceHistoryAsync(updated);
                }
                catch
                {
                    // price history tracking must not block transaction updates
                }
            }

            return ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private async Task RecordPriceHistoryAsync(Transaction transaction)
        {
            var property = await _propertyRepository.GetByIdAsync(transaction.PropertyId);
            if (property?.Address is null || property.SurfaceArea <= 0) return;

            var period = DateTime.UtcNow.ToString("yyyy-MM");
            var propertyType = property.Type.ToString();

            var existing = await _priceHistoryRepository.GetByPostalCodeAsync(property.Address.PostalCode);
            var alreadyRecorded = existing.Any(p =>
                p.City.Equals(property.Address.City, StringComparison.OrdinalIgnoreCase) &&
                p.Period == period &&
                p.PropertyType == propertyType);

            if (alreadyRecorded) return;

            await _priceHistoryRepository.CreateAsync(new PriceHistory
            {
                City = property.Address.City,
                PostalCode = property.Address.PostalCode,
                PropertyType = propertyType,
                AveragePricePerM2 = Math.Round(transaction.FinalPrice / property.SurfaceArea, 2),
                TransactionCount = 1,
                Period = period,
                Source = "plazzo-platform"
            });
        }

        private static TransactionResponse ToResponse(Transaction t) => new()
        {
            Id = t.Id,
            PropertyId = t.PropertyId,
            OfferId = t.OfferId,
            SellerId = t.SellerId,
            BuyerId = t.BuyerId,
            FinalPrice = t.FinalPrice,
            CompromiseDate = t.CompromiseDate,
            DeedDate = t.DeedDate,
            Notary = t.Notary,
            Status = t.Status,
            CreatedAt = t.CreatedAt
        };
    }