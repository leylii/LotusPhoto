namespace LotusPhoto.Api.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Photo
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Title { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Category { get; set; }

        [Required]
        public required string PreviewUrl { get; set; }    // visible to everyone with lower resolution

        [Required]
        public required string OriginalPath { get; set; }  // secure file, original photo, not public

        public decimal Price { get; set; }

        [Required]
        public Currency Currency { get; set; } = Currency.NOK;

        [MaxLength(500)]
        public string? Description { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateTaken { get; set; }
    }
}
