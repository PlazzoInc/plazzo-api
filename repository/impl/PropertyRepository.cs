using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class PropertyRepository : IPropertyRepository
    {
        private readonly PlazzoContext _context;

        public PropertyRepository(PlazzoContext context)
        {
            _context = context;
        }

        private IQueryable<Property> WithIncludes() =>
            _context.Properties
                .Include(p => p.Address)
                .Include(p => p.Photos)
                .Include(p => p.Features);

        public async Task<List<Property>> GetAllAsync()
        {
            return await WithIncludes().AsNoTracking().ToListAsync();
        }

        public async Task<List<Property>> GetByAgencyIdAsync(int agencyId)
        {
            return await WithIncludes()
                .AsNoTracking()
                .Where(p => p.AgencyId == agencyId)
                .ToListAsync();
        }

        public async Task<List<Property>> GetByCommercialIdAsync(int commercialId)
        {
            return await WithIncludes()
                .AsNoTracking()
                .Where(p => p.CommercialId == commercialId)
                .ToListAsync();
        }

        public async Task<Property?> GetByIdAsync(int id)
        {
            return await WithIncludes().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Property> CreateAsync(Property property)
        {
            _context.Properties.Add(property);
            await _context.SaveChangesAsync();
            return property;
        }

        public async Task<Property?> UpdateAsync(Property property)
        {
            var existing = await WithIncludes().FirstOrDefaultAsync(p => p.Id == property.Id);
            if (existing is null) return null;

            existing.Type = property.Type;
            existing.Status = property.Status;
            existing.Title = property.Title;
            existing.Description = property.Description;
            existing.Price = property.Price;
            existing.SurfaceArea = property.SurfaceArea;
            existing.RoomCount = property.RoomCount;
            existing.BedroomCount = property.BedroomCount;
            existing.BathroomCount = property.BathroomCount;
            existing.Floor = property.Floor;
            existing.ConstructionYear = property.ConstructionYear;
            existing.EnergyRating = property.EnergyRating;
            existing.UpdatedAt = DateTime.UtcNow;

            if (existing.Address is not null && property.Address is not null)
            {
                existing.Address.Address = property.Address.Address;
                existing.Address.City = property.Address.City;
                existing.Address.PostalCode = property.Address.PostalCode;
                existing.Address.Department = property.Address.Department;
                existing.Address.Region = property.Address.Region;
                existing.Address.Latitude = property.Address.Latitude;
                existing.Address.Longitude = property.Address.Longitude;
            }
            else if (property.Address is not null)
            {
                property.Address.PropertyId = existing.Id;
                existing.Address = property.Address;
            }

            _context.PropertyFeatures.RemoveRange(existing.Features);
            existing.Features.Clear();
            foreach (var feature in property.Features)
            {
                feature.PropertyId = existing.Id;
                existing.Features.Add(feature);
            }

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.Properties.FindAsync(id);
            if (existing is null) return false;

            _context.Properties.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }
    }