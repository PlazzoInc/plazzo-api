using Microsoft.AspNetCore.Http;
using plazzo_api.dto.response;

namespace plazzo_api.service;

public interface IPropertyPhotoService
{
    Task<PropertyPhotoResponse?> UploadAsync(int propertyId, IFormFile file, byte order, string caption);
    Task<bool> DeleteAsync(int photoId);
}