using plazzo_api.dto.request.properties;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using plazzo_api.service;

namespace plazzo_api.controller;
[ApiController]
    [Route("api/properties/{propertyId}/photos")]
    [Authorize(Roles = "Admin,Commercial")]
    public class PropertyPhotosController : ControllerBase
    {
        private readonly IPropertyPhotoService _service;

        public PropertyPhotosController(IPropertyPhotoService service)
        {
            _service = service;
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload(int propertyId, [FromForm] UploadPropertyPhotoRequest request)
        {
            if (request.File is null || request.File.Length == 0)
                return BadRequest(new { message = "No file uploaded." });

            try
            {
                var result = await _service.UploadAsync(propertyId, request.File, request.Order, request.Caption);
                return result is null ? NotFound(new { message = "Property not found." }) : Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{photoId}")]
        public async Task<IActionResult> Delete(int propertyId, int photoId)
        {
            var deleted = await _service.DeleteAsync(photoId);
            return deleted ? NoContent() : NotFound();
        }
    }