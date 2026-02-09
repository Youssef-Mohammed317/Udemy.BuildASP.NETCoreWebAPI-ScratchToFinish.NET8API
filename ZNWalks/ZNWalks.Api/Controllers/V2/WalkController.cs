using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Threading.Tasks;
using NZWalks.Api.CustomActionFilters;
using NZWalks.Application.DTOs.Common.Filtering;
using NZWalks.Application.DTOs.Common.Paginate;
using NZWalks.Application.DTOs.Common.Sorting;
using NZWalks.Application.DTOs.WalkDTOs;
using NZWalks.Application.Interfaces;

namespace NZWalks.Api.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class WalkController : ControllerBase
    {
        private readonly IWalkService walkService;

        public WalkController(IWalkService _walkService)
        {
            walkService = _walkService;
        }

        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll(
            [FromQuery] FilterParam? filterParam = null,
            [FromQuery] SortParam? sortParam = null,
            [FromQuery] PaginateParam? paginateParam = null)
        {
            var walks = await walkService.GetAllAsync(
                filterParam,
                sortParam,
                paginateParam);

            return Ok(walks);
        }

        [HttpPost("search")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> Search([FromBody] GetAllRequestDto requestDto)
        {
            var walks = await walkService.SearchWalkAsync(requestDto);

            return Ok(walks);
        }
        [HttpPost("search/details")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> SearchWithDetails([FromBody] GetAllRequestDto requestDto)
        {
            var walks = await walkService.SearchWalkWithDetailsAsync(requestDto);

            return Ok(walks);
        }


        [HttpGet]
        [Route("details")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllWithDetails(
            [FromQuery] FilterParam? filterParam = null,
            [FromQuery] SortParam? sortParam = null,
            [FromQuery] PaginateParam? paginateParam = null)
        {
            var walks = await walkService.GetAllWithDetailsAsync(
                filterParam,
                sortParam,
                paginateParam);

            return Ok(walks);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var walk = await walkService.GetByIdAsync(id);

            return walk == null ? NotFound() : Ok(walk);
        }
        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id)
        {
            var walk = await walkService.DeleteByIdAsync(id);

            return walk == null ? NotFound() : Ok(walk);
        }
        [HttpPut("{id:guid}")]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateWalkDto walkDto)
        {
            var walk = await walkService.UpdateAsync(id, walkDto);

            return walk == null ? NotFound() : Ok(walk);
        }
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] CreateWalkDto walkDto)
        {
            var walk = await walkService.CreateAsync(walkDto);
            return walk == null ? NotFound() : Ok(walk);
        }

    }
}
