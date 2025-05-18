namespace LotusPhoto.Api.Repositories
{
    using LotusPhoto.Api.Models;
    public interface IPhotoRepository
    {
        Task<IEnumerable<Photo>> GetAllAsync();
        Task<IEnumerable<Photo>> GetAfterIdAsync(int lastId, int count);
        Task<IEnumerable<Photo>> GetPagedAsync(int pageNumber, int pageSize);
        Task<Photo?> GetByIdAsync(int id);
        Task AddAsync(Photo photo);
        Task UpdateAsync(Photo photo);
        Task DeleteAsync(int id);
    }
}
