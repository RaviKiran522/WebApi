using System.ComponentModel.DataAnnotations;

namespace NZwalks.Models.DTOs
{
    public class UpdateRegionDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum 3 charecters required")]
        [MaxLength(15, ErrorMessage = "Maximum 15 charecters required")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
