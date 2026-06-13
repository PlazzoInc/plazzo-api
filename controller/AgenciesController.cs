using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.agencies;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/agencies")]
    public class AgenciesController : ControllerBase
    {
        private readonly IAgencyService _service;

        public AgenciesController(IAgencyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var agency = await _service.GetByIdAsync(id);
            return agency is null ? NotFound() : Ok(agency);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAgencyRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAgencyRequest request)
        {
            var updated = await _service.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
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