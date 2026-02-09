using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZWalks.Application.DTOs.AuthDTOs.LoginDTOs
{
    public class LoginSuccessResponseDto : LoginResponseDto
    {
        public string? UserId { get; set; }
        public string? Token { get; set; }
    }
}
