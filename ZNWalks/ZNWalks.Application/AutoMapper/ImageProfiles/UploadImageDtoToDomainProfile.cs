using AutoMapper;
using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.DifficultyDTOs;
using NZWalks.Application.DTOs.ImageDTOs;
using NZWalks.Domain.Models;

namespace NZWalks.Application.AutoMapper.ImageProfiles
{
    public class UploadImageDtoToDomainProfile : Profile
    {
        public UploadImageDtoToDomainProfile()
        {
            CreateMap<UploadImageRequestDto, Image>()
                .ForMember(dest => dest.File, opt => opt.MapFrom(src => src.File))
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
                .ForMember(dest => dest.FileExtension, opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName)))
                .ForMember(dest => dest.FileSizeInBytes, opt => opt.MapFrom(src => src.File.Length))
                .ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.File.ContentType))
                .ForMember(dest => dest.FileDescrpition, opt => opt.MapFrom(src => src.FileDescription));
        }
    }
}
