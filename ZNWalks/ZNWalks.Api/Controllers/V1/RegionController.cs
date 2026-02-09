using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NZWalks.Api.CustomActionFilters;
using NZWalks.Application.DTOs.RegionDTOs;
using NZWalks.Application.Interfaces;

namespace NZWalks.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly IRegionService regionServices;

        public RegionController(IRegionService _regionServices)
        {
            regionServices = _regionServices;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await regionServices.GetAllAsync();

            return Ok(regions);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var region = await regionServices.GetByIdAsync(id);

            return region == null ? NotFound() : Ok(region);
        }

        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CreateRegionDto createRegionDto)
        {
            var region = await regionServices.CreateAsync(createRegionDto);
            return region == null ? NotFound() : CreatedAtAction(nameof(GetById), new { Id = region.Id }, region);
        }

        [HttpPut("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionDto updateRegionDto)
        {
            var region = await regionServices.UpdateAsync(id, updateRegionDto);

            return region == null ? NotFound() : Ok(region);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var region = await regionServices.DeleteByIdAsync(id);

            return region == null ? NotFound() : Ok(region);
        }
    }
}
