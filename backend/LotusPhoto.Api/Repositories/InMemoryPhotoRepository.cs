namespace LotusPhoto.Api.Repositories
{
    using LotusPhoto.Api.Models;

    //TODO: Soon this will be replaced with a real database
    public class InMemoryPhotoRepository : IPhotoRepository
    {
        private readonly List<Photo> _photos = new()
        {
            new Photo { Id = 0, Title = "shine", Category = "Portrait", PreviewUrl = "/images/previews/img1.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 19.99m },
            new Photo { Id = 1, Title = "branches", Category = "Landscape", PreviewUrl = "/images/previews/img2.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 11.99m },
            new Photo { Id = 2, Title = "hope", Category = "Landscape", PreviewUrl = "/images/previews/img3.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 10.99m },
            new Photo { Id = 3, Title = "illusion", Category = "Nature", PreviewUrl = "/images/previews/img4.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 11.99m },
            new Photo { Id = 5, Title = "far away", Category = "Portrait", PreviewUrl = "/images/previews/img6.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 11.99m },
            new Photo { Id = 6, Title = "near", Category = "Portrait", PreviewUrl = "/images/previews/img7.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 13.99m },
            new Photo { Id = 7, Title = "all together", Category = "Landscape", PreviewUrl = "/images/previews/img8.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 11.99m },
            new Photo { Id = 8, Title = "dream home", Category = "Landscape", PreviewUrl = "/images/previews/img9.jpeg", OriginalPath = "SecureStorage/img1.jpeg" , Price = 11.99m},
            new Photo { Id = 9, Title = "sky", Category = "Landscape", PreviewUrl = "/images/previews/img10.jpeg", OriginalPath = "SecureStorage/img1.jpeg", Price = 11.99m },
        };

        public Task<IEnumerable<Photo>> GetAllAsync()
        {
            return Task.FromResult(_photos.AsEnumerable());
        }

        public Task<IEnumerable<Photo>> GetAfterIdAsync(int lastId, int count)
        {
            var result = _photos
                .Where(p => p.Id > lastId)
                .OrderBy(p => p.Id)
                .Take(count);

            return Task.FromResult(result);
        }

        public Task<IEnumerable<Photo>> GetPagedAsync(int pageNumber, int pageSize)
        {
            var result = _photos
                .OrderBy(p => p.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            return Task.FromResult(result);
        }

        public Task<Photo?> GetByIdAsync(int id)
        {
            return Task.FromResult(_photos.FirstOrDefault(p => p.Id == id));
        }

        public Task AddAsync(Photo photo)
        {
            photo.Id = _photos.Any() ? _photos.Max(p => p.Id) + 1 : 0;
            _photos.Add(photo);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Photo photo)
        {
            var photo2Update = _photos.FirstOrDefault(p => p.Id == photo.Id);
            if (photo2Update == null) return Task.CompletedTask;

            photo2Update.Title = photo.Title;
            photo2Update.Category = photo.Category;
            photo2Update.PreviewUrl = photo.PreviewUrl;
            photo2Update.OriginalPath = photo.OriginalPath;
            photo2Update.Price = photo.Price;
            photo2Update.Description = photo.Description;
            photo2Update.DateTaken = photo.DateTaken;

            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var photo = _photos.FirstOrDefault(p => p.Id == id);
            if (photo != null)
                _photos.Remove(photo);

            return Task.CompletedTask;
        }
    }
}
