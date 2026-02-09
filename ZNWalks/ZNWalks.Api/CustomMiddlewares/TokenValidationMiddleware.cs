using NZWalks.Application.Interfaces;
using NZWalks.Application.Services;

namespace NZWalks.Api.CustomMiddlewares
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenValidationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tokenService = context.RequestServices.GetRequiredService<ITokenService>();

            if (context.User?.Identity?.IsAuthenticated == true)
            {
                var jti = context.User.FindFirst("jti")?.Value;
                if (!string.IsNullOrEmpty(jti))
                {
                    var isValid = await tokenService.IsTokenValidAsync(jti);
                    if (!isValid)
                    {
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Token revoked or invalid");
                        return;
                    }
                }
            }

            await _next(context);
        }
    }
}
