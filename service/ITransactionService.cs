using plazzo_api.dto.request.transactions;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface ITransactionService
    {
        Task<List<TransactionResponse>> GetAllAsync(int? commercialId = null);
        Task<TransactionResponse?> GetByIdAsync(int id);
        Task<TransactionResponse> CreateAsync(CreateTransactionRequest request);
        Task<TransactionResponse?> UpdateAsync(int id, UpdateTransactionRequest request);
        Task<bool> DeleteAsync(int id);
    }