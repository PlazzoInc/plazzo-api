using plazzo_api.dto.request.agencies;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl
{
    public class AgencyService : IAgencyService
    {
        private readonly IAgencyRepository _repository;

        public AgencyService(IAgencyRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AgencyResponse>> GetAllAsync()
        {
            var agencies = await _repository.GetAllAsync();
            return agencies.Select(ToResponse).ToList();
        }

        public async Task<AgencyResponse?> GetByIdAsync(int id)
        {
            var agency = await _repository.GetByIdAsync(id);
            return agency is null ? null : ToResponse(agency);
        }

        public async Task<AgencyResponse> CreateAsync(CreateAgencyRequest request)
        {
            var agency = new Agency
            {
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Phone = request.Phone,
                Email = request.Email
            };

            var created = await _repository.CreateAsync(agency);
            return ToResponse(created);
        }

        public async Task<AgencyResponse?> UpdateAsync(int id, UpdateAgencyRequest request)
        {
            var agency = new Agency
            {
                Id = id,
                Name = request.Name,
                Address = request.Address,
                City = request.City,
                PostalCode = request.PostalCode,
                Phone = request.Phone,
                Email = request.Email
            };

            var updated = await _repository.UpdateAsync(agency);
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static AgencyResponse ToResponse(Agency agency) => new()
        {
            Id = agency.Id,
            Name = agency.Name,
            Address = agency.Address,
            City = agency.City,
            PostalCode = agency.PostalCode,
            Phone = agency.Phone,
            Email = agency.Email,
            CreatedAt = agency.CreatedAt
        };
    }
}