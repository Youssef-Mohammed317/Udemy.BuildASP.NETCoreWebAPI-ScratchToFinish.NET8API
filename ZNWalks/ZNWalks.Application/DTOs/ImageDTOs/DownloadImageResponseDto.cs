using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Application.DTOs.ImageDTOs
{
    public class DownloadImageResponseDto : ImageResponseDto
    {
        public byte[] Content { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}
