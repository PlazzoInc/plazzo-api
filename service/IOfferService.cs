using plazzo_api.dto.request.offers;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IOfferService
    {
        Task<List<OfferResponse>> GetAllAsync(int? commercialId = null);
        Task<OfferResponse?> GetByIdAsync(int id);
        Task<OfferResponse> CreateAsync(CreateOfferRequest request, int buyerId);
        Task<OfferResponse?> UpdateAsync(int id, UpdateOfferRequest request);
        Task<bool> DeleteAsync(int id);
    }