using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.RegionDTOs;
using NZWalks.Application.Interfaces;
using NZWalks.Domain.Interfaces;
using NZWalks.Domain.Models;

namespace NZWalks.Application.Services
{
    public class RegionService : IRegionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RegionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<RegionDto> CreateAsync(CreateRegionDto regionDto)
        {
            var region = _mapper.Map<Region>(regionDto);

            await _unitOfWork.RegionRepository.CreateAsync(region);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<RegionDto?> DeleteByIdAsync(Guid id)
        {
            var region = await _unitOfWork.RegionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return null!;
            }
            _unitOfWork.RegionRepository.Delete(region);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RegionDto>(region);
        }

        public async Task<IEnumerable<RegionDto>> GetAllAsync()
        {
            var regions = await _unitOfWork.RegionRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<RegionDto>>(regions);
        }
        public async Task<RegionDto?> GetByIdAsync(Guid id)
        {
            var region = await _unitOfWork.RegionRepository.GetByIdAsync(id);
            if (region != null)
            {
                return _mapper.Map<RegionDto>(region);
            }
            return null!;
        }

        public async Task<RegionDto?> UpdateAsync(Guid id, UpdateRegionDto regionDto)
        {
            var region = await _unitOfWork.RegionRepository.GetByIdAsync(id);
            if (region == null)
            {
                return null!;
            }
            _mapper.Map(regionDto, region);

            _unitOfWork.RegionRepository.Update(region);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<RegionDto>(region);
        }
    }
}
