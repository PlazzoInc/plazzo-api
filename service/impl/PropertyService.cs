using plazzo_api.dto.request.properties;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _repository;

        public PropertyService(IPropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<PropertyResponse>> GetAllAsync()
        {
            var properties = await _repository.GetAllAsync();
            return properties.Select(ToResponse).ToList();
        }

        public async Task<PropertyResponse?> GetByIdAsync(int id)
        {
            var property = await _repository.GetByIdAsync(id);
            return property is null ? null : ToResponse(property);
        }

        public async Task<PropertyResponse> CreateAsync(
            CreatePropertyRequest request, int currentUserId, int? currentUserAgencyId, bool isAdmin)
        {
            var agencyId = isAdmin && request.AgencyId.HasValue
                ? request.AgencyId.Value
                : currentUserAgencyId ?? throw new InvalidOperationException(
                    "Current user has no agency and no AgencyId was provided.");

            var commercialId = isAdmin && request.CommercialId.HasValue
                ? request.CommercialId.Value
                : currentUserId;

            var property = new Property
            {
                AgencyId = agencyId,
                CommercialId = commercialId,
                Type = request.Type,
                Status = request.Status,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                SurfaceArea = request.SurfaceArea,
                RoomCount = request.RoomCount,
                BedroomCount = request.BedroomCount,
                BathroomCount = request.BathroomCount,
                Floor = request.Floor,
                ConstructionYear = request.ConstructionYear,
                EnergyRating = request.EnergyRating,
                UpdatedAt = DateTime.UtcNow,
                Address = new PropertyAddress
                {
                    Address = request.Address.Address,
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    Department = request.Address.Department,
                    Region = request.Address.Region,
                    Latitude = request.Address.Latitude,
                    Longitude = request.Address.Longitude
                },
                Features = request.Features.Select(f => new PropertyFeature
                {
                    Key = f.Key,
                    Value = f.Value
                }).ToList()
            };

            var created = await _repository.CreateAsync(property);
            return ToResponse(created);
        }

        public async Task<PropertyResponse?> UpdateAsync(int id, UpdatePropertyRequest request)
        {
            var property = new Property
            {
                Id = id,
                Type = request.Type,
                Status = request.Status,
                Title = request.Title,
                Description = request.Description,
                Price = request.Price,
                SurfaceArea = request.SurfaceArea,
                RoomCount = request.RoomCount,
                BedroomCount = request.BedroomCount,
                BathroomCount = request.BathroomCount,
                Floor = request.Floor,
                ConstructionYear = request.ConstructionYear,
                EnergyRating = request.EnergyRating,
                Address = new PropertyAddress
                {
                    Address = request.Address.Address,
                    City = request.Address.City,
                    PostalCode = request.Address.PostalCode,
                    Department = request.Address.Department,
                    Region = request.Address.Region,
                    Latitude = request.Address.Latitude,
                    Longitude = request.Address.Longitude
                },
                Features = request.Features.Select(f => new PropertyFeature
                {
                    Key = f.Key,
                    Value = f.Value
                }).ToList()
            };

            var updated = await _repository.UpdateAsync(property);
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static PropertyResponse ToResponse(Property p) => new()
        {
            Id = p.Id,
            AgencyId = p.AgencyId,
            CommercialId = p.CommercialId,
            Type = p.Type,
            Status = p.Status,
            Title = p.Title,
            Description = p.Description,
            Price = p.Price,
            SurfaceArea = p.SurfaceArea,
            RoomCount = p.RoomCount,
            BedroomCount = p.BedroomCount,
            BathroomCount = p.BathroomCount,
            Floor = p.Floor,
            ConstructionYear = p.ConstructionYear,
            EnergyRating = p.EnergyRating,
            CreatedAt = p.CreatedAt,
            UpdatedAt = p.UpdatedAt,
            Address = p.Address is null ? null : new PropertyAddressResponse
            {
                Address = p.Address.Address,
                City = p.Address.City,
                PostalCode = p.Address.PostalCode,
                Department = p.Address.Department,
                Region = p.Address.Region,
                Latitude = p.Address.Latitude,
                Longitude = p.Address.Longitude
            },
            Photos = p.Photos.OrderBy(ph => ph.Order).Select(ph => new PropertyPhotoResponse
            {
                Id = ph.Id,
                Url = ph.Url,
                Order = ph.Order,
                Caption = ph.Caption
            }).ToList(),
            Features = p.Features.Select(f => new PropertyFeatureResponse
            {
                Id = f.Id,
                Key = f.Key,
                Value = f.Value
            }).ToList()
        };
    }