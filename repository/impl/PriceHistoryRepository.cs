using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly PlazzoContext _context;

        public PriceHistoryRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<PriceHistory>> GetAllAsync()
        {
            return await _context.PriceHistories.AsNoTracking().ToListAsync();
        }

        public async Task<List<PriceHistory>> GetByCityAsync(string city)
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Where(p => p.City.ToLower() == city.ToLower())
                .OrderBy(p => p.Period)
                .ToListAsync();
        }

        public async Task<List<PriceHistory>> GetByPostalCodeAsync(string postalCode)
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Where(p => p.PostalCode == postalCode)
                .OrderBy(p => p.Period)
                .ToListAsync();
        }

        public async Task<List<PriceHistory>> GetByPeriodAsync(string period)
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Where(p => p.Period == period)
                .ToListAsync();
        }

        public async Task<PriceHistory> CreateAsync(PriceHistory priceHistory)
        {
            _context.PriceHistories.Add(priceHistory);
            await _context.SaveChangesAsync();
            return priceHistory;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.PriceHistories.FindAsync(id);
            if (existing is null) return false;

            _context.PriceHistories.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }