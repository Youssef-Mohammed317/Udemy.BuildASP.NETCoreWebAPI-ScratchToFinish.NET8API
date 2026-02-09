using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using NZWalks.Application.DTOs.AuthDTOs;
using NZWalks.Application.DTOs.AuthDTOs.LoginDTOs;
using NZWalks.Application.DTOs.AuthDTOs.RegisterDTOs;
using NZWalks.Application.Interfaces;
using NZWalks.Domain.Interfaces;
using NZWalks.Infra.Identity.Domian.Interfaces;
using NZWalks.Infra.Identity.Domian.Models;

namespace NZWalks.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthUnitOfWork unitOfWork;
        private readonly IConfiguration configuration;

        public AuthService(IAuthUnitOfWork _unitOfWork, IConfiguration _configuration)
        {
            unitOfWork = _unitOfWork;
            configuration = _configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await unitOfWork.AuthRepository.FindByEmailAsync(loginRequestDto.Username);

            if (user == null)
            {
                return new LoginFailureResponseDto
                {
                    Success = false,
                    Message = "User Not Found"
                };
            }

            var result = await unitOfWork.AuthRepository.CheckPasswordAsync(user, loginRequestDto.Password);

            if (result == false)
            {
                return new LoginFailureResponseDto
                {
                    Success = false,
                    Message = "Username or password inccorect!"
                };
            }

            var roles = await unitOfWork.AuthRepository.GetUserRolesAsync(user);

            var claims = CreateClaims(user, roles);

            var token = CreateTokenString(claims);

            await unitOfWork.TokenRepository.AddTokenAsync(new TokenStore
            {
                UserId = user.Id,
                Jti = claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value,
                ExpiryDate = DateTime.UtcNow.AddMinutes(15),
                Token = token,
                IsRevoked = false
            });

            return new LoginSuccessResponseDto
            {
                Success = true,
                Message = "Login successfully!",
                Token = token,
                UserId = user.Id
            };
        }

        public async Task LogoutAsync(string jti)
        {
            await unitOfWork.TokenRepository.RevokeTokenAsync(jti);
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            if (registerRequestDto.Roles == null || !registerRequestDto.Roles.Any())
            {
                return new RegisterFailureResponseDto
                {
                    Success = false,
                    Message = "There is no Roles!, Please check your data"
                };
            }

            var roles = await unitOfWork.AuthRepository.GetRolesAsync();

            foreach (var role in registerRequestDto.Roles)
            {
                if (!roles.Contains(role.ToUpper()))
                {
                    return new RegisterFailureResponseDto
                    {
                        Success = false,
                        Message = "There is invaild roles sent!, Please check your data"
                    };
                }
            }

            var user = new ApplicationUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var result = await unitOfWork.AuthRepository.CreateUserAsync(user, registerRequestDto.Password);

            if (!result.Succeeded)
            {
                return new RegisterFailureResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }


            result = await unitOfWork.AuthRepository.AddUserToRolesAsync(user, registerRequestDto.Roles);
            if (!result.Succeeded)
            {

                return new RegisterFailureResponseDto
                {
                    Success = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }

            return new RegisterSuccessResponseDto
            {
                Success = true,
                Message = "User created successfully",
                UserId = user.Id
            };
        }

        private IEnumerable<Claim> CreateClaims(ApplicationUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email!));


            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
        private string CreateTokenString(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));

            var credentails = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                 issuer: configuration["JWT:Issuer"]!,
                 audience: configuration["JWT:Audience"]!,
                 claims: claims,
                 expires: DateTime.UtcNow.AddMinutes(15),
                 signingCredentials: credentails);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }
    }
}
