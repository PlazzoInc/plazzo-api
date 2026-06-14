using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.analytics;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/price-history")]
    public class PriceHistoryController : ControllerBase
    {
        private readonly IPriceHistoryService _service;

        public PriceHistoryController(IPriceHistoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("city/{city}")]
        public async Task<IActionResult> GetByCity(string city)
        {
            return Ok(await _service.GetByCityAsync(city));
        }

        [HttpGet("postal/{postalCode}")]
        public async Task<IActionResult> GetByPostalCode(string postalCode)
        {
            return Ok(await _service.GetByPostalCodeAsync(postalCode));
        }

        [HttpGet("period/{period}")]
        public async Task<IActionResult> GetByPeriod(string period)
        {
            return Ok(await _service.GetByPeriodAsync(period));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePriceHistoryRequest request)
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