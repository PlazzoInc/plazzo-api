using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class MandateRepository : IMandateRepository
    {
        private readonly PlazzoContext _context;

        public MandateRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<Mandate>> GetAllAsync()
        {
            return await _context.Mandates.AsNoTracking().ToListAsync();
        }

        public async Task<Mandate?> GetByIdAsync(int id)
        {
            return await _context.Mandates.FindAsync(id);
        }

        public async Task<Mandate> CreateAsync(Mandate mandate)
        {
            _context.Mandates.Add(mandate);
            await _context.SaveChangesAsync();
            return mandate;
        }

        public async Task<Mandate?> UpdateAsync(Mandate mandate)
        {
            var existing = await _context.Mandates.FindAsync(mandate.Id);
            if (existing is null) return null;

            existing.Type = mandate.Type;
            existing.StartDate = mandate.StartDate;
            existing.EndDate = mandate.EndDate;
            existing.FeePercentage = mandate.FeePercentage;
            existing.Status = mandate.Status;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Mandates.FindAsync(id);
            if (existing is null) return false;

            _context.Mandates.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }