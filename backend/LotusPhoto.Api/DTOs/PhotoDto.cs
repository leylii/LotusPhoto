namespace LotusPhoto.Api.DTOs
{
    using System.ComponentModel.DataAnnotations;

    public class PhotoDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = default!;

        [Required]
        [MaxLength(50)]
        public string Category { get; set; } = default!;

        [Required]
        public string PreviewUrl { get; set; } = default!;

        public decimal Price { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }
    }
}
