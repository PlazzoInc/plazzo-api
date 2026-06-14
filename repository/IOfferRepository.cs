using plazzo_api.entity;

namespace plazzo_api.repository;
public interface IOfferRepository
    {
        Task<List<Offer>> GetAllAsync();
        Task<Offer?> GetByIdAsync(int id);
        Task<Offer> CreateAsync(Offer offer);
        Task<Offer?> UpdateAsync(Offer offer);
        Task<bool> DeleteAsync(int id);
    }