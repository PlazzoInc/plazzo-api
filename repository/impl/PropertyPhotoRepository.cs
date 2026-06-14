using Microsoft.EntityFrameworkCore;
using plazzo_api.dbContext;
using plazzo_api.entity;

namespace plazzo_api.repository.impl;
public class PropertyPhotoRepository : IPropertyPhotoRepository
    {
        private readonly PlazzoContext _context;

        public PropertyPhotoRepository(PlazzoContext context)
        {
            _context = context;
        }

        public async Task<PropertyPhoto?> GetByIdAsync(int id)
        {
            return await _context.PropertyPhotos.FindAsync(id);
        }

        public async Task<PropertyPhoto> CreateAsync(PropertyPhoto photo)
        {
            _context.PropertyPhotos.Add(photo);
            await _context.SaveChangesAsync();
            return photo;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.PropertyPhotos.FindAsync(id);
            if (existing is null) return false;

            _context.PropertyPhotos.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PropertyExistsAsync(int propertyId)
        {
            return await _context.Properties.AnyAsync(p => p.Id == propertyId);
        }
    }