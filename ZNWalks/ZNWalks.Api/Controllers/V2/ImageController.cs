using Asp.Versioning;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NZWalks.Api.CustomActionFilters;
using NZWalks.Application.DTOs.ImageDTOs;
using NZWalks.Application.Interfaces;

namespace NZWalks.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly IImageService imageService;

        public ImageController(IImageService _imageService)
        {
            imageService = _imageService;
        }

        [HttpPost("Upload")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Upload([FromForm] UploadImageRequestDto request)
        {

            var result = await imageService.UploadImageAsync(request);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Delete/{id:guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {

            var result = await imageService.DeleteImageAsync(id);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Download/{id:guid}")]
        [Authorize(Roles = "Writer")]
        [ValidateModel]
        public async Task<IActionResult> Download([FromRoute] Guid id)
        {

            var result = await imageService.DownLoadImageAsync(id);

            if (result.Success && result is DownloadImageResponseDto data)
            {
                return File(data.Content, data.ContentType, data.FileName);
            }

            return BadRequest(result);
        }
    }
}
