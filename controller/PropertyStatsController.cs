using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/stats")]
    public class PropertyStatsController : ControllerBase
    {
        private readonly IPropertyStatsService _service;

        public PropertyStatsController(IPropertyStatsService service)
        {
            _service = service;
        }

        [HttpGet("properties/{propertyId}")]
        public async Task<IActionResult> GetByPropertyId(int propertyId)
        {
            var stats = await _service.GetOrCreateAsync(propertyId);
            return Ok(stats);
        }

        [HttpGet("top/views")]
        public async Task<IActionResult> GetTopByViews([FromQuery] int count = 10)
        {
            return Ok(await _service.GetTopByViewsAsync(count));
        }

        [HttpGet("top/offers")]
        public async Task<IActionResult> GetTopByOffers([FromQuery] int count = 10)
        {
            return Ok(await _service.GetTopByOffersAsync(count));
        }

        [HttpPost("properties/{propertyId}/view")]
        public async Task<IActionResult> IncrementView(int propertyId)
        {

            await _service.GetOrCreateAsync(propertyId);
            var stats = await _service.IncrementViewAsync(propertyId);
            return stats is null ? NotFound() : Ok(stats);
        }
    }
}