namespace LotusPhoto.Api.Mappers
{
    using LotusPhoto.Api.DTOs;
    using LotusPhoto.Api.Models;

    public static class PhotoMapper
    {
        public static PhotoDto ToDto(this Photo photo)
        {
            return new PhotoDto
            {
                Id = photo.Id,
                Title = photo.Title,
                Category = photo.Category,
                PreviewUrl = photo.PreviewUrl,
                Price = photo.Price,
                Description = photo.Description
            };
        }

        public static Photo ToModel(this PhotoDto dto)
        {
            string photoName = System.IO.Path.GetFileName(dto.PreviewUrl);
            return new Photo
            {
                Id = dto.Id,
                Title = dto.Title,
                Category = dto.Category,
                PreviewUrl = dto.PreviewUrl,
                Price = dto.Price,
                Description = dto.Description,
                OriginalPath = "SecureStorage/" + photoName
            };
        }
    }
}
