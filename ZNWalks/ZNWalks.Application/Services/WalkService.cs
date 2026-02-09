using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.Common.Filtering;
using NZWalks.Application.DTOs.Common.Paginate;
using NZWalks.Application.DTOs.Common.Sorting;
using NZWalks.Application.DTOs.WalkDTOs;
using NZWalks.Application.Interfaces;
using NZWalks.Domain.Interfaces;
using NZWalks.Domain.Models;

namespace NZWalks.Application.Services
{
    public class WalkService : IWalkService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public WalkService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<WalkDto> CreateAsync(CreateWalkDto walkDto)
        {
            var walk = _mapper.Map<Walk>(walkDto);

            await _unitOfWork.WalkRepository.CreateAsync(walk);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<WalkDto>(walk);
        }

        public async Task<WalkDetailsDto?> DeleteByIdAsync(Guid id)
        {
            var walk = await _unitOfWork.WalkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return null;
            }

            _unitOfWork.WalkRepository.Delete(walk);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<WalkDetailsDto>(walk);

        }

        public async Task<IEnumerable<WalkDto>> GetAllAsync(
            FilterParam? filterParam = null,
            SortParam? sortParam = null,
            PaginateParam? paginateParam = null)
        {
            var walks = _unitOfWork.WalkRepository.GetAll();

            walks = FilterSortPaginate(walks, new GetAllRequestDto
            {
                filterParams = [filterParam],
                sortParams = [sortParam],
                paginateParam = paginateParam
            });

            return _mapper.Map<IEnumerable<WalkDto>>(await walks.ToListAsync());
        }
        public async Task<IEnumerable<WalkDetailsDto>> GetAllWithDetailsAsync(
                FilterParam? filterParam = null,
                SortParam? sortParam = null,
                PaginateParam? paginateParam = null)
        {
            var walks = _unitOfWork.WalkRepository.GetAll();

            walks = FilterSortPaginate(walks, new GetAllRequestDto
            {
                filterParams = [filterParam],
                sortParams = [sortParam],
                paginateParam = paginateParam
            });

            walks = GetDetails(walks);

            return _mapper.Map<IEnumerable<WalkDetailsDto>>(await walks.ToListAsync());
        }

        public async Task<WalkDetailsDto?> GetByIdAsync(Guid id)
        {
            var walk = await _unitOfWork.WalkRepository.GetByIdAsync(id);
            if (walk == null)
            {
                return null;
            }
            return _mapper.Map<WalkDetailsDto>(walk);
        }

        public async Task<IEnumerable<WalkDto>> SearchWalkAsync(GetAllRequestDto requestDto)
        {
            var walks = _unitOfWork.WalkRepository.GetAll();

            walks = FilterSortPaginate(walks, requestDto);

            walks = GetDetails(walks);

            return _mapper.Map<IEnumerable<WalkDto>>(await walks.ToListAsync());
        }
        public async Task<IEnumerable<WalkDetailsDto>> SearchWalkWithDetailsAsync(GetAllRequestDto requestDto)
        {
            var walks = _unitOfWork.WalkRepository.GetAll();

            walks = FilterSortPaginate(walks, requestDto);

            walks = GetDetails(walks);

            return _mapper.Map<IEnumerable<WalkDetailsDto>>(await walks.ToListAsync());
        }

        public async Task<WalkDetailsDto?> UpdateAsync(Guid id, UpdateWalkDto walkDto)
        {
            var walk = await _unitOfWork.WalkRepository.GetByIdAsync(id);

            if (walk == null)
            {
                return null;
            }

            _mapper.Map(walkDto, walk);

            _unitOfWork.WalkRepository.Update(walk);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<WalkDetailsDto>(walk);
        }


        private IQueryable<Walk> FilterSortPaginate(IQueryable<Walk> walks, GetAllRequestDto requestDto)
        {

            // filter
            walks = FilterHelper.Filter<Walk>(walks, requestDto.filterParams);

            // sort
            walks = SortHelper.Sort<Walk>(walks, requestDto.sortParams);

            // pagenation
            int pageSize = requestDto?.paginateParam?.PageSize ?? 10;

            int skipResult = (pageSize * (requestDto?.paginateParam?.PageNumber - 1)) ?? 0;

            walks = walks.Skip(skipResult).Take(pageSize);

            return walks;
        }

        private IQueryable<Walk> GetDetails(IQueryable<Walk> walks)
        {
            return walks.Include(w => w.Difficulty).Include(w => w.Region);
        }
    }
}
