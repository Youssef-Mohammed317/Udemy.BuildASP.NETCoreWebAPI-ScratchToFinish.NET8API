using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.DifficultyDTOs;
using NZWalks.Application.Interfaces;
using NZWalks.Domain.Interfaces;
using NZWalks.Domain.Models;

namespace NZWalks.Application.Services
{
    public class DifficultyService : IDifficultyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DifficultyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DifficultyDto> CreateAsync(CreateDifficultyDto difficultyDto)
        {

            var difficulty = _mapper.Map<Difficulty>(difficultyDto);

            await _unitOfWork.DifficultyRepository.CreateAsync(difficulty);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DifficultyDto>(difficulty);
        }

        public async Task<DifficultyDto?> DeleteByIdAsync(Guid id)
        {
            var difficulty = await _unitOfWork.DifficultyRepository.GetByIdAsync(id);
            if (difficulty == null)
            {
                return null!;
            }
            _unitOfWork.DifficultyRepository.Delete(difficulty);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DifficultyDto>(difficulty);
        }

        public async Task<IEnumerable<DifficultyDto>> GetAllAsync()
        {
            var difficulties = await _unitOfWork.DifficultyRepository.GetAll().ToListAsync();

            return _mapper.Map<IEnumerable<DifficultyDto>>(difficulties);
        }

        public async Task<DifficultyDto?> GetByIdAsync(Guid id)
        {
            var difficulty = await _unitOfWork.DifficultyRepository.GetByIdAsync(id);
            if (difficulty == null)
            {
                return null!;
            }
            return _mapper.Map<DifficultyDto>(difficulty);
        }

        public async Task<DifficultyDto?> UpdateAsync(Guid id, UpdateDifficultyDto difficultyDto)
        {
            var difficulty = await _unitOfWork.DifficultyRepository.GetByIdAsync(id);
            if (difficulty == null)
            {
                return null!;
            }
            _mapper.Map(difficultyDto, difficulty);

            _unitOfWork.DifficultyRepository.Update(difficulty);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DifficultyDto>(difficulty);
        }
    }
}
