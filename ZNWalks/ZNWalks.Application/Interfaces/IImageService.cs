using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.ImageDTOs;

namespace NZWalks.Application.Interfaces
{
    public interface IImageService
    {
        Task<ImageResponseDto> UploadImageAsync(UploadImageRequestDto request);

        Task<ImageResponseDto> DeleteImageAsync(Guid id);

        Task<ImageResponseDto> DownLoadImageAsync(Guid id);

    }
}
