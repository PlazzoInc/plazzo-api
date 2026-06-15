using plazzo_api.dto.request.mandates;
using plazzo_api.dto.response;

namespace plazzo_api.service;
public interface IMandateService
    {
        Task<List<MandateResponse>> GetAllAsync(int? commercialId = null);
        Task<MandateResponse?> GetByIdAsync(int id);
        Task<MandateResponse> CreateAsync(CreateMandateRequest request);
        Task<MandateResponse?> UpdateAsync(int id, UpdateMandateRequest request);
        Task<bool> DeleteAsync(int id);
    }