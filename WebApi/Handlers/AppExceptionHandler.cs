using ApplicationCore.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebApi.Handlers;

public class AppExceptionHandler(ProblemDetailsFactory factory, ILogger<AppExceptionHandler> logger): IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is MovieNotFoundException || exception is UserNotFoundException)
        {
            logger.Log(LogLevel.Information, $"Exception '{exception.Message}' handled!");
            var problem = factory.CreateProblemDetails(
                httpContext,
                StatusCodes.Status400BadRequest,
                "Error during adding new review!",
                "Service error",
                detail: exception.Message
            );
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
            return true;
        }

        return false;
    }
}