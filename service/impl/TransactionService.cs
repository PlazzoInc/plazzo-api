using plazzo_api.dto.request.transactions;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _repository;

        public TransactionService(ITransactionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TransactionResponse>> GetAllAsync()
        {
            var transactions = await _repository.GetAllAsync();
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
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
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