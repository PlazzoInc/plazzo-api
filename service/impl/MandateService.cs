using plazzo_api.dto.request.mandates;
using plazzo_api.dto.response;
using plazzo_api.entity;
using plazzo_api.repository;

namespace plazzo_api.service.impl;
public class MandateService : IMandateService
    {
        private readonly IMandateRepository _repository;

        public MandateService(IMandateRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<MandateResponse>> GetAllAsync()
        {
            var mandates = await _repository.GetAllAsync();
            return mandates.Select(ToResponse).ToList();
        }

        public async Task<MandateResponse?> GetByIdAsync(int id)
        {
            var mandate = await _repository.GetByIdAsync(id);
            return mandate is null ? null : ToResponse(mandate);
        }

        public async Task<MandateResponse> CreateAsync(CreateMandateRequest request)
        {
            var mandate = new Mandate
            {
                PropertyId = request.PropertyId,
                ClientId = request.ClientId,
                CommercialId = request.CommercialId,
                Type = request.Type,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                FeePercentage = request.FeePercentage,
                Status = MandateStatus.Active
            };

            var created = await _repository.CreateAsync(mandate);
            return ToResponse(created);
        }

        public async Task<MandateResponse?> UpdateAsync(int id, UpdateMandateRequest request)
        {
            var mandate = new Mandate
            {
                Id = id,
                Type = request.Type,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                FeePercentage = request.FeePercentage,
                Status = request.Status
            };

            var updated = await _repository.UpdateAsync(mandate);
            return updated is null ? null : ToResponse(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        private static MandateResponse ToResponse(Mandate m) => new()
        {
            Id = m.Id,
            PropertyId = m.PropertyId,
            ClientId = m.ClientId,
            CommercialId = m.CommercialId,
            Type = m.Type,
            StartDate = m.StartDate,
            EndDate = m.EndDate,
            FeePercentage = m.FeePercentage,
            Status = m.Status,
            CreatedAt = m.CreatedAt
        };
    }