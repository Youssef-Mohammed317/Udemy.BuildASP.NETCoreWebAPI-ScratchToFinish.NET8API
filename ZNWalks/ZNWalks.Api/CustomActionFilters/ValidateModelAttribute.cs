using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NZWalks.Api.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {

            var errors = context.ModelState
                .Where(ms => ms.Value?.Errors?.Count > 0)
                .Select(ms => new
                {
                    Field = ms.Key,
                    Errors = ms.Value?.Errors?.Select(e => e.ErrorMessage).ToArray()
                });

                if (errors.Any())
                {
                    var response = new
                    {
                        Success = false,
                        Message = "Validation failed.",
                        Errors = errors
                    };

                    context.Result = new BadRequestObjectResult(response);
                }
            }

        


            
        }
    }
}
