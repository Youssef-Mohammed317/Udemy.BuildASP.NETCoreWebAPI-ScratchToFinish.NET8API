using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.RegionDTOs;
using NZWalks.Domain.Models;

namespace NZWalks.Application.AutoMapper.RegionProfiles
{
    public class CreateRegionDtoToDomainProfile : Profile
    {
        public CreateRegionDtoToDomainProfile()
        {
            CreateMap<CreateRegionDto, Region>();
        }
    }
}
