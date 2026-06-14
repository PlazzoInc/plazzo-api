using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class PropertyStatsRepository : IPropertyStatsRepository
    {
        private readonly PlazzoContext _context;

        public PropertyStatsRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<PropertyStats?> GetByPropertyIdAsync(int propertyId)
        {
            return await _context.PropertyStats
                .FirstOrDefaultAsync(s => s.PropertyId == propertyId);
        }

        public async Task<List<PropertyStats>> GetTopByViewsAsync(int count)
        {
            return await _context.PropertyStats
                .AsNoTracking()
                .OrderByDescending(s => s.ViewCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<PropertyStats>> GetTopByOffersAsync(int count)
        {
            return await _context.PropertyStats
                .AsNoTracking()
                .OrderByDescending(s => s.OfferCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<PropertyStats> CreateAsync(PropertyStats stats)
        {
            _context.PropertyStats.Add(stats);
            await _context.SaveChangesAsync();
            return stats;
        }

        public async Task<PropertyStats?> IncrementViewAsync(int propertyId)
        {
            var stats = await _context.PropertyStats
                .FirstOrDefaultAsync(s => s.PropertyId == propertyId);

            if (stats is null) return null;

            stats.ViewCount++;
            stats.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return stats;
        }

        public async Task<PropertyStats?> UpdateAsync(PropertyStats stats)
        {
            var existing = await _context.PropertyStats
                .FirstOrDefaultAsync(s => s.PropertyId == stats.PropertyId);

            if (existing is null) return null;

            existing.ViewCount = stats.ViewCount;
            existing.FavoriteCount = stats.FavoriteCount;
            existing.VisitCount = stats.VisitCount;
            existing.OfferCount = stats.OfferCount;
            existing.DaysOnMarket = stats.DaysOnMarket;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existing;
        }
    }