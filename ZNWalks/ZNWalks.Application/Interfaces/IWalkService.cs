using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.Common.Filtering;
using NZWalks.Application.DTOs.Common.Paginate;
using NZWalks.Application.DTOs.Common.Sorting;
using NZWalks.Application.DTOs.WalkDTOs;

namespace NZWalks.Application.Interfaces
{
    public interface IWalkService
    {
        public Task<IEnumerable<WalkDto>> GetAllAsync(
                FilterParam? filterParam = null,
                SortParam? sortParam = null,
                PaginateParam? paginate = null
            );
        public Task<IEnumerable<WalkDetailsDto>> GetAllWithDetailsAsync(
                FilterParam? filterParam = null,
                 SortParam? sortParam = null,
                PaginateParam? paginateParam = null
            );

        public Task<IEnumerable<WalkDto>> SearchWalkAsync(GetAllRequestDto requestDto);
        public Task<IEnumerable<WalkDetailsDto>> SearchWalkWithDetailsAsync(GetAllRequestDto requestDto);
        public Task<WalkDetailsDto?> GetByIdAsync(Guid id);
        public Task<WalkDetailsDto?> DeleteByIdAsync(Guid id);
        public Task<WalkDetailsDto?> UpdateAsync(Guid id, UpdateWalkDto walkDto);
        public Task<WalkDto> CreateAsync(CreateWalkDto walkDto);
    }
}
