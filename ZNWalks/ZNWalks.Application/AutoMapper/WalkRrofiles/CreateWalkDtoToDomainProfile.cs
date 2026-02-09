using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.WalkDTOs;
using NZWalks.Domain.Models;

namespace NZWalks.Application.AutoMapper.WalkRrofiles
{
    public class CreateWalkDtoToDomainProfile : Profile
    {
        public CreateWalkDtoToDomainProfile()
        {
            CreateMap<CreateWalkDto, Walk>();
        }
    }
}
