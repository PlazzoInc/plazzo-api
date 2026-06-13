using plazzo_api.dto.request.agencies;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IAgencyService
    {
        Task<List<AgencyResponse>> GetAllAsync();
        Task<AgencyResponse?> GetByIdAsync(int id);
        Task<AgencyResponse> CreateAsync(CreateAgencyRequest request);
        Task<AgencyResponse?> UpdateAsync(int id, UpdateAgencyRequest request);
        Task<bool> DeleteAsync(int id);
    }