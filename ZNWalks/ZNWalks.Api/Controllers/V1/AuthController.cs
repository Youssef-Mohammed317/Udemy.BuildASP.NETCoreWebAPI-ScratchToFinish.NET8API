using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using NZWalks.Api.CustomActionFilters;
using NZWalks.Application.DTOs.AuthDTOs.LoginDTOs;
using NZWalks.Application.DTOs.AuthDTOs.RegisterDTOs;
using NZWalks.Application.Interfaces;

namespace NZWalks.Api.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;

        public AuthController(IAuthService _authService)
        {
            authService = _authService;
        }

        [HttpPost("Register")]
        [ValidateModel]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var result = await authService.RegisterAsync(registerRequestDto);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpPost("Login")]
        [ValidateModel]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await authService.LoginAsync(loginRequestDto);

            return result.Success ? Ok(result) : BadRequest(result);
        }
        [HttpPost("Logout")]
        [ValidateModel]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var jti = User.FindFirstValue(JwtRegisteredClaimNames.Jti);

            if (string.IsNullOrEmpty(jti))
                return BadRequest("Invalid token.");

            await authService.LogoutAsync(jti);

            return Ok(new { message = "User logged out successfully." });
        }

    }
}
