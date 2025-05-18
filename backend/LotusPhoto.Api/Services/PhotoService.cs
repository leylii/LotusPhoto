namespace LotusPhoto.Api.Services
{
    using LotusPhoto.Api.Models;
    using LotusPhoto.Api.Repositories;
    using Microsoft.Extensions.Logging;

    public class PhotoService : IPhotoService
    {
        private readonly IPhotoRepository _repository;
        private readonly ILogger<PhotoService> _logger;

        public PhotoService(IPhotoRepository repository, ILogger<PhotoService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Photo>> GetAllAsync()
        {
            try
            {
                var photos = await _repository.GetAllAsync();
                _logger.LogInformation("Fetched all photos for gallery display");
                return photos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all photos");
                throw;
            }
        }

        public async Task<IEnumerable<Photo>> GetAfterIdAsync(int lastId, int count)
        {
            if (count <= 0)
            {
                _logger.LogWarning("Invalid count in GetAfterIdAsync: {Count}", count);
                throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero");
            }

            if (lastId < 0)
            {
                _logger.LogWarning("Invalid lastId in GetAfterIdAsync: {LastId}", lastId);
                throw new ArgumentOutOfRangeException(nameof(lastId), "LastId cannot be negative");
            }

            try
            {
                var photos = await _repository.GetAfterIdAsync(lastId, count);
                _logger.LogInformation("Loaded {Count} photos after ID {LastId}", photos.Count(), lastId);
                return photos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching photos after ID {LastId}", lastId);
                throw;
            }
        }

        public async Task<IEnumerable<Photo>> GetPagedAsync(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                _logger.LogWarning("Invalid page number: {PageNumber}", pageNumber);
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be at least 1");
            }

            if (pageSize <= 0)
            {
                _logger.LogWarning("Invalid page size: {PageSize}", pageSize);
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than zero");
            }

            try
            {
                var photos = await _repository.GetPagedAsync(pageNumber, pageSize);
                _logger.LogInformation("Loaded {Count} photos for page {PageNumber}", photos.Count(), pageNumber);
                return photos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching paged photos: page {PageNumber}, size {PageSize}", pageNumber, pageSize);
                throw;
            }
        }

        public async Task<Photo?> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                _logger.LogWarning("Invalid ID in GetByIdAsync: {Id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be non-negative");
            }

            try
            {
                var photo = await _repository.GetByIdAsync(id);

                if (photo == null)
                {
                    _logger.LogInformation("Photo with ID {Id} not found", id);
                }
                else
                {
                    _logger.LogInformation("Photo with ID {Id} retrieved successfully", id);
                }

                return photo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving photo with ID {Id}", id);
                throw;
            }
        }

        public async Task AddAsync(Photo photo)
        {
            if (string.IsNullOrWhiteSpace(photo.Title))
            {
                _logger.LogWarning("Validation failed: Title is required");
                throw new ArgumentException("Title is required");
            }

            if (string.IsNullOrWhiteSpace(photo.Category))
            {
                _logger.LogWarning("Validation failed: Category is required");
                throw new ArgumentException("Category is required");
            }

            if (string.IsNullOrWhiteSpace(photo.PreviewUrl))
            {
                _logger.LogWarning("Validation failed: PreviewUrl is required");
                throw new ArgumentException("PreviewUrl is required.");
            }               

            if (string.IsNullOrWhiteSpace(photo.OriginalPath))
            {
                _logger.LogWarning("Validation failed: OriginalPath is required");
                throw new ArgumentException("OriginalPath is required.");
            }
                
            if (photo.Price <= 0)
            {
                _logger.LogWarning("Validation failed: Price must be greater than 0");
                throw new ArgumentException("Price must be greater than 0");
            }

            photo.DateTaken = photo.DateTaken?.Date;

            try
            {
                await _repository.AddAsync(photo);
                _logger.LogInformation("Photo added: {Title}", photo.Title);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding photo: {Title}", photo.Title);
                throw;
            }
        }

        public async Task UpdateAsync(Photo photo)
        {
            if (photo.Id < 0)
            {
                _logger.LogWarning("Invalid photo ID: {Id}", photo.Id);
                throw new ArgumentOutOfRangeException(nameof(photo.Id), "Photo ID must be non-negative");
            }

            if (string.IsNullOrWhiteSpace(photo.Title))
            {
                _logger.LogWarning("Validation failed: Title is required");
                throw new ArgumentException("Title is required");
            }

            if (photo.Price <= 0)
            {
                _logger.LogWarning("Validation failed: Price must be greater than 0");
                throw new ArgumentException("Price must be greater than 0");
            }

            var existing = await _repository.GetByIdAsync(photo.Id);
            if (existing == null)
            {
                _logger.LogWarning("Cannot update non-existing photo: {Id}", photo.Id);
                throw new InvalidOperationException($"Photo with ID {photo.Id} does not exist");
            }

            photo.DateTaken = photo.DateTaken?.Date;

            try
            {
                await _repository.UpdateAsync(photo);
                _logger.LogInformation("Photo updated: {Id}", photo.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating photo: {Id}", photo.Id);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            if (id < 0)
            {
                _logger.LogWarning("Invalid ID for deletion: {Id}", id);
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be non-negative");
            }

            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
            {
                _logger.LogWarning("Attempted to delete non-existing photo: {Id}", id);
                throw new InvalidOperationException($"Photo with ID {id} does not exist");
            }

            try
            {
                await _repository.DeleteAsync(id);
                _logger.LogInformation("Photo deleted: {Id}", id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting photo: {Id}", id);
                throw;
            }
        }
    }
}
