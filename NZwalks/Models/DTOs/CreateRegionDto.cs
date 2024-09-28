using System.ComponentModel.DataAnnotations;

namespace NZwalks.Models.DTOs
{
    public class CreateRegionDto
    {
        [Required] // default Error: The Code field is required. (custom Error: [Required(ErrorMessage="Needed")]
        public string Code { get; set; }

        [Required(ErrorMessage = "this is needed")] // custom error
        [MinLength(3, ErrorMessage = "Minimum 3 charecters required")]
        [MaxLength(15, ErrorMessage = "Maximum 15 charecters required")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
