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

        public List<string>? Tags { get; set; }
        public string? Location { get; set; }
        public CameraSettings? CameraSettings { get; set; }
        public string? Orientation { get; set; } 
        public bool? IsForSale { get; set; }
        public int? ViewCount { get; set; }
        public int? DownloadCount { get; set; }
        public string? PhotographerName { get; set; }
        public string? Dimensions { get; set; }
        public string? LicenseType { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
