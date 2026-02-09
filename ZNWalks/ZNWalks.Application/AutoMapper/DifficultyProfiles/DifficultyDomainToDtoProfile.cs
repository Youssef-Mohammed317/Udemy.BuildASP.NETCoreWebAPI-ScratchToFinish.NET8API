using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.DifficultyDTOs;
using NZWalks.Domain.Models;

namespace NZWalks.Application.AutoMapper.DifficultyProfiles
{
    public class DifficultyDomainToDtoProfile : Profile
    {
        public DifficultyDomainToDtoProfile()
        {
            CreateMap<Difficulty, DifficultyDto>();
        }
    }
}
