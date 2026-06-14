using Microsoft.AspNetCore.Http;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class PropertyPhotoService : IPropertyPhotoService
    {
        private readonly IPropertyPhotoRepository _repository;
        private readonly IWebHostEnvironment _env;

        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp" };
        private const long MaxFileSizeBytes = 5 * 1024 * 1024; // 5 MB

        public PropertyPhotoService(IPropertyPhotoRepository repository, IWebHostEnvironment env)
        {
            _repository = repository;
            _env = env;
        }

        public async Task<PropertyPhotoResponse?> UploadAsync(int propertyId, IFormFile file, byte order, string caption)
        {
            if (!await _repository.PropertyExistsAsync(propertyId))
                return null;

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
                throw new InvalidOperationException("Unsupported file type. Allowed: jpg, jpeg, png, webp.");

            if (file.Length > MaxFileSizeBytes)
                throw new InvalidOperationException("File too large. Max size is 5 MB.");

            var uploadsRoot = Path.Combine(_env.WebRootPath ?? "wwwroot", "uploads", "properties", propertyId.ToString());
            Directory.CreateDirectory(uploadsRoot);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(uploadsRoot, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"/uploads/properties/{propertyId}/{fileName}";

            var photo = new PropertyPhoto
            {
                PropertyId = propertyId,
                Url = relativeUrl,
                Order = order,
                Caption = caption
            };

            var created = await _repository.CreateAsync(photo);

            return new PropertyPhotoResponse
            {
                Id = created.Id,
                Url = created.Url,
                Order = created.Order,
                Caption = created.Caption
            };
        }

        public async Task<bool> DeleteAsync(int photoId)
        {
            var photo = await _repository.GetByIdAsync(photoId);
            if (photo is null) return false;

            var deleted = await _repository.DeleteAsync(photoId);

            if (deleted)
            {
                var filePath = Path.Combine(_env.WebRootPath ?? "wwwroot", photo.Url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }

            return deleted;
        }
    }