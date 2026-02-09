using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.AuthDTOs.LoginDTOs;
using NZWalks.Application.DTOs.AuthDTOs.RegisterDTOs;

namespace NZWalks.Application.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loignRequestDto);
        Task LogoutAsync(string jti);
    }
}
