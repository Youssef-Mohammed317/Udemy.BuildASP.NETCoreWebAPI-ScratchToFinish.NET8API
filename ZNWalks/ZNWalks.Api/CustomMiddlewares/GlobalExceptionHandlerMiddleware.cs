using System.Net;
using System.Threading.Tasks;

namespace NZWalks.Api.CustomMiddlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate _next,
            ILogger<GlobalExceptionHandlerMiddleware> _logger)
        {
            next = _next;
            logger = _logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();

                logger.LogError($"{errorId} : {ex.Message}");

                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = "Something went wrong!, We are looking into resolving this."
                };

                await response.WriteAsJsonAsync(error);

            }



        }
    }
}
