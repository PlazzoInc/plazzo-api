using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.dto.request.properties;
using plazzo_api.service;

namespace plazzo_api.controller
{
    [ApiController]
    [Route("api/properties")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyService _service;
        private readonly IUserService _userService;

        public PropertiesController(IPropertyService service, IUserService userService)
        {
            _service = service;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Public endpoint — no auth required for listing
            // But if logged in as Commercial, filter by agency
            if (User.Identity?.IsAuthenticated == true)
            {
                var role = User.FindFirst(ClaimTypes.Role)?.Value;
                if (role == "Commercial")
                {
                    var agencyIdClaim = User.FindFirst("agencyId")?.Value;
                    int? agencyId = string.IsNullOrEmpty(agencyIdClaim) ? null
                        : int.TryParse(agencyIdClaim, out var aid) ? aid : null;
                    return Ok(await _service.GetAllAsync(agencyId: agencyId));
                }
            }
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var property = await _service.GetByIdAsync(id);
            return property is null ? NotFound() : Ok(property);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> Create([FromBody] CreatePropertyRequest request)
        {
            var currentUserId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
            var isAdmin = User.IsInRole("Admin");

            var currentUser = await _userService.GetByIdAsync(currentUserId);
            if (currentUser is null) return Unauthorized();

            try
            {
                var created = await _service.CreateAsync(request, currentUserId, currentUser.AgencyId, isAdmin);
                return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePropertyRequest request)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "Commercial")
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
                var property = await _service.GetByIdAsync(id);
                if (property is null) return NotFound();
                if (property.CommercialId != userId)
                    return Forbid(); // Commercial can't edit another commercial's property
            }
            var updated = await _service.UpdateAsync(id, request);
            return updated is null ? NotFound() : Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,Commercial")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (role == "Commercial")
            {
                var userId = int.Parse(User.Claims.First(c => c.Type == JwtRegisteredClaimNames.Sub).Value);
                var property = await _service.GetByIdAsync(id);
                if (property is null) return NotFound();
                if (property.CommercialId != userId)
                    return Forbid();
            }
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}