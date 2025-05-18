namespace LotusPhoto.Api.Controllers
{
    using LotusPhoto.Api.DTOs;
    using LotusPhoto.Api.Mappers;
    using LotusPhoto.Api.Services;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _service;
        private readonly ILogger<PhotoController> _logger;

        public PhotoController(IPhotoService service, ILogger<PhotoController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetAll()
        {
            _logger.LogInformation("Fetching all photos for gallery dispaly");

            var photos = await _service.GetAllAsync();
            return Ok(photos.Select(p => p.ToDto()));
        }

        [HttpGet("after/{lastId:int}")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetAfter(int lastId, [FromQuery] int count = 10)
        {
            _logger.LogInformation("Fetching {Count} photos after ID {LastId}", count, lastId);

            var photos = await _service.GetAfterIdAsync(lastId, count);
            return Ok(photos.Select(p => p.ToDto()));
        }

        [HttpGet("page/{pageNumber:int}")]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPaged(int pageNumber, [FromQuery] int pageSize = 10)
        {
            _logger.LogInformation("Fetching page {PageNumber} of photos with page size: {PageSize}", pageNumber, pageSize);

            var photos = await _service.GetPagedAsync(pageNumber, pageSize);
            return Ok(photos.Select(p => p.ToDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PhotoDto>> GetById(int id)
        {
            _logger.LogInformation("Looking up photo by ID {Id}", id);

            var photo = await _service.GetByIdAsync(id);
            if (photo == null)
            {
                _logger.LogWarning("Photo with ID {Id} not found", id);
                return NotFound(new { message = $"No photo found with ID {id}" });
            }

            return Ok(photo.ToDto());
        }

        [HttpPost]
        public async Task<ActionResult<PhotoDto>> Add([FromBody] PhotoDto dto)
        {
            _logger.LogInformation("Adding new photo with title: '{Title}'", dto.Title);

            var model = dto.ToModel();
            await _service.AddAsync(model);

            _logger.LogInformation("Photo added successfully with ID: {Id}", model.Id);

            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model.ToDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] PhotoDto dto)
        {
            if (id != dto.Id)
            {
                _logger.LogWarning("Photo failed due to ID mismatch (URL: {UrlId}, Body: {BodyId})", id, dto.Id);
                return BadRequest(new { message = "ID in URL doesn't match ID in body" });
            }

            _logger.LogInformation("Updating photo with ID: {Id}", id);

            try
            {
                var model = dto.ToModel();
                await _service.UpdateAsync(model);
                _logger.LogInformation("Photo updated successfully with ID: {Id}", id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Update failed — photo not found");
                return NotFound(new { message = $"Photo with ID: {id} not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while updating photo");
                return StatusCode(500, "An unexpected error occurred while updating the photo");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting photo with ID: {Id}", id);

            try
            {
                await _service.DeleteAsync(id);
                _logger.LogInformation("Photo deleted successfully with ID: {Id}", id);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Delete failed - photo not found");
                return NotFound(new { message = $"Photo with ID {id} not found" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error while deleting photo");
                return StatusCode(500, "An unexpected error occurred while deleting the photo");
            }
        }
    }
}
