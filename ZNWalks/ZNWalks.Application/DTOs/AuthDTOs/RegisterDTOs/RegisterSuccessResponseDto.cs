using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Application.DTOs.AuthDTOs.RegisterDTOs
{
    public class RegisterSuccessResponseDto : RegisterResponseDto
    {
        public string? UserId { get; set; }
    }
}
