using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class AgencyRepository : IAgencyRepository
    {
        private readonly PlazzoContext _context;

        public AgencyRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<List<Agency>> GetAllAsync()
        {
            return await _context.Agencies.AsNoTracking().ToListAsync();
        }

        public async Task<Agency?> GetByIdAsync(int id)
        {
            return await _context.Agencies.FindAsync(id);
        }

        public async Task<Agency> CreateAsync(Agency agency)
        {
            _context.Agencies.Add(agency);
            await _context.SaveChangesAsync();
            return agency;
        }

        public async Task<Agency?> UpdateAsync(Agency agency)
        {
            var existing = await _context.Agencies.FindAsync(agency.Id);
            if (existing is null) return null;

            existing.Name = agency.Name;
            existing.Address = agency.Address;
            existing.City = agency.City;
            existing.PostalCode = agency.PostalCode;
            existing.Phone = agency.Phone;
            existing.Email = agency.Email;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Agencies.FindAsync(id);
            if (existing is null) return false;

            _context.Agencies.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }