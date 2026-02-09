using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Domain.Models;

namespace NZWalks.Application.DTOs.WalkDTOs
{
    public class UpdateWalkDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name Must Be Less Than 100 Characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Name Must Be Less Than 1000 Characters")]

        public string Description { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Length Must Be Less Than 100 Km")]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }
        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
