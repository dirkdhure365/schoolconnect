using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FluentValidation.Results;

namespace SchoolConnect.Common.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Where(x => x.Value?.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                );

            var validationFailures = errors.SelectMany(kvp => 
                kvp.Value.Select(error => new ValidationFailure(kvp.Key, error))).ToList();

            var validationException = new Application.Exceptions.ValidationException(validationFailures);

            context.Result = new BadRequestObjectResult(new
            {
                StatusCode = 400,
                Message = validationException.Message,
                Errors = validationException.Errors,
                Timestamp = DateTime.UtcNow
            });

            return;
        }

        await next();
    }
}
