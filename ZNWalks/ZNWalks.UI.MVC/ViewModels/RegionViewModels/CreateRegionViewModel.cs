using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.UI.MVC.Models.RegionDTOs
{
    public class CreateRegionViewModel
    {
        [Required]
        [MaxLength(3, ErrorMessage = "Code Must Be 3 Characters")]
        [MinLength(3, ErrorMessage = "Code Must Be 3 Characters")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Name Must Be Less Than 100 Characters")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
