using AutoMapper;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.ImageDTOs;
using NZWalks.Application.Interfaces;
using NZWalks.Domain.Interfaces;
using NZWalks.Domain.Models;
namespace NZWalks.Application.Services
{
    public class ImageService : IImageService
    {
        private readonly long maxFileSize = 10 * 1024 * 1024; // 10MB
        private readonly string[] allowedExtensions = [".jpg", ".png", ".jpeg"];

        private readonly IUnitOfWork unitOfWork;
        private readonly IHostEnvironment hostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public ImageService(IUnitOfWork _unitOfWork, IHostEnvironment _hostEnvironment, IHttpContextAccessor _httpContextAccessor, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            hostEnvironment = _hostEnvironment;
            httpContextAccessor = _httpContextAccessor;
            mapper = _mapper;
        }
        public async Task<ImageResponseDto> DeleteImageAsync(Guid id)
        {
            var imageDomain = await unitOfWork.ImageRepository.GetByIdAsync(id);

            if (imageDomain == null || !File.Exists(imageDomain.LocalFilePath))
            {
                return new DeleteImageFailureResponseDto
                {
                    Success = false,
                    Message = "This image does not exist!, Please check your data."
                };
            }

            File.Delete(imageDomain.LocalFilePath);

            unitOfWork.ImageRepository.Delete(imageDomain);

            return new DeleteImageSuccessResponseDto
            {
                Success = true,
                Message = "This image deleted successfully!"
            };

        }

        public async Task<ImageResponseDto> DownLoadImageAsync(Guid id)
        {
            var imageDomain = await unitOfWork.ImageRepository.GetByIdAsync(id);

            if (imageDomain == null || !File.Exists(imageDomain.LocalFilePath))
            {
                return new DeleteImageFailureResponseDto
                {
                    Success = false,
                    Message = "This image does not exist!, Please check your data."
                };
            }

            var imageContent = await File.ReadAllBytesAsync(imageDomain.LocalFilePath);

            if(imageContent == null)
            {
                return new DeleteImageFailureResponseDto
                {
                    Success = false,
                    Message = "Something went wrong!, please try again."
                };
            }

            return new DownloadImageResponseDto
            {
                Success = true,
                Message = "File Strating Download Successfully",
                Content = imageContent,
                FileName = imageDomain.FileName,
                ContentType = imageDomain.FileType
            };

        }
        public async Task<ImageResponseDto> UploadImageAsync(UploadImageRequestDto request)
        {

            ImageResponseDto result = ValidateFileUpload(request);

            if (!result.Success)
            {
                return result;
            }
            var imageDomain = mapper.Map<Image>(request);

            imageDomain.StoredFileName = Guid.NewGuid();

            imageDomain = await AddFilePathesToImageAsync(imageDomain);

            await unitOfWork.ImageRepository.CreateAsync(imageDomain);

            await unitOfWork.SaveChangesAsync();

            return new UploadImageSuccessResponseDto
            {
                Success = true,
                Message = "File Uploaded Successfully",
                FilePath = imageDomain.FilePath,
                FileSizeInBytes = imageDomain.FileSizeInBytes,
                FileName = imageDomain.FileName,
                File = imageDomain.File,
                FileDescrpition = imageDomain.FileDescrpition,
                FileExtension = imageDomain.FileExtension,
                Id = imageDomain.Id
            };
        }

        private ImageResponseDto ValidateFileUpload(UploadImageRequestDto request)
        {
            var fileExtension = Path.GetExtension(request.File.FileName);

            if (!allowedExtensions.Contains(fileExtension))
            {
                return new UploadImageFailureResponseDto
                {
                    Success = false,
                    Message = "Unsupported file extension"
                };
            }

            if (request.File.Length > maxFileSize)
            {
                return new UploadImageFailureResponseDto
                {
                    Success = false,
                    Message = "File size is more than 10MB"
                };
            }


            return new UploadImageFailureResponseDto
            {
                Success = true,
                Message = "File Validated Successfully"
            };
        }

        private async Task<Image> AddFilePathesToImageAsync(Image imageDomain)
        {

            var localFilePath = Path.Combine(hostEnvironment.ContentRootPath, "Images",
                $"{imageDomain.StoredFileName}{imageDomain.FileExtension}");

            using var stream = new FileStream(localFilePath, FileMode.Create);
            await imageDomain.File.CopyToAsync(stream);
            var httpRequest = httpContextAccessor.HttpContext.Request;
            var urlFilePath =
                $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}/Images/{imageDomain.StoredFileName}{imageDomain.FileExtension}";

            imageDomain.FilePath = urlFilePath;
            imageDomain.LocalFilePath = localFilePath;

            return imageDomain;
        }


    }
}
