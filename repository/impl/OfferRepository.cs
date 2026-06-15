using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class OfferRepository : IOfferRepository
    {
        private readonly PlazzoContext _context;

        public OfferRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<Offer>> GetAllAsync()
        {
            return await _context.Offers.AsNoTracking().ToListAsync();
        }

        public async Task<List<Offer>> GetByCommercialPropertyAsync(int commercialId)
        {
            return await _context.Offers
                .AsNoTracking()
                .Where(o => _context.Properties
                    .Any(p => p.Id == o.PropertyId && p.CommercialId == commercialId))
                .ToListAsync();
        }

        public async Task<Offer?> GetByIdAsync(int id)
        {
            return await _context.Offers.FindAsync(id);
        }

        public async Task<Offer> CreateAsync(Offer offer)
        {
            _context.Offers.Add(offer);
            await _context.SaveChangesAsync();
            return offer;
        }

        public async Task<Offer?> UpdateAsync(Offer offer)
        {
            var existing = await _context.Offers.FindAsync(offer.Id);
            if (existing is null) return null;

            existing.Status = offer.Status;
            existing.Amount = offer.Amount;
            existing.Message = offer.Message;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Offers.FindAsync(id);
            if (existing is null) return false;

            _context.Offers.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }