using NZwalks.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace NZwalks.Models.DTOs
{
    public class CreateWalkDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1,50)]
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
