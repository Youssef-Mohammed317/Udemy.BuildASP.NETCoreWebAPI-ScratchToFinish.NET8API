using System.Net;

namespace NZWalks.UI.MVC.CustomMiddleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid().ToString();
                // log real error


                var response = context.Response;

                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                response.ContentType = "application/json";

                var error = new
                {
                    Id = errorId,
                    ErrorMessage = ex.Message, // "Something went wrong!, We are looking into resolving this."
                };

                await response.WriteAsJsonAsync(error);
            }
        }
    }
}
