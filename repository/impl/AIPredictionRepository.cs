using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class AIPredictionRepository : IAIPredictionRepository
    {
        private readonly PlazzoContext _context;

        public AIPredictionRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<AIPrediction>> GetAllAsync()
        {
            return await _context.AIPredictions.AsNoTracking().ToListAsync();
        }

        public async Task<List<AIPrediction>> GetByPropertyIdAsync(int propertyId)
        {
            return await _context.AIPredictions
                .AsNoTracking()
                .Where(p => p.PropertyId == propertyId)
                .ToListAsync();
        }

        public async Task<List<AIPrediction>> GetByTypeAsync(PredictionType type)
        {
            return await _context.AIPredictions
                .AsNoTracking()
                .Where(p => p.Type == type)
                .ToListAsync();
        }

        public async Task<AIPrediction> CreateAsync(AIPrediction prediction)
        {
            _context.AIPredictions.Add(prediction);
            await _context.SaveChangesAsync();
            return prediction;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.AIPredictions.FindAsync(id);
            if (existing is null) return false;

            _context.AIPredictions.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }