using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.offers;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/offers")]
    [Authorize]
    public class OffersController : ControllerBase
    {
        private readonly IOfferService _service;

        public OffersController(IOfferService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> GetAll()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "Commercial")
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
                return Ok(await _service.GetAllAsync(commercialId: userId));
            }
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var offer = await _service.GetByIdAsync(id);
            return offer is null ? NotFound() : Ok(offer);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOfferRequest request)
        {
            var buyerId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            var created = await _service.CreateAsync(request, buyerId);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateOfferRequest request)
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