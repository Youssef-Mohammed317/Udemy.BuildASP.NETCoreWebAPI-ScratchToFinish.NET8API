using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Application.DTOs.AuthDTOs.LoginDTOs
{
    public abstract class LoginResponseDto
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
    }
}
