using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.analytics;
using plazzo_api.entity;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/predictions")]
    public class AIPredictionController : ControllerBase
    {
        private readonly IAIPredictionService _service;

        public AIPredictionController(IAIPredictionService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetByProperty(int propertyId)
        {
            return Ok(await _service.GetByPropertyIdAsync(propertyId));
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetByType(PredictionType type)
        {
            return Ok(await _service.GetByTypeAsync(type));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAIPredictionRequest request)
        {
            var created = await _service.CreateAsync(request);
            return Ok(created);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}