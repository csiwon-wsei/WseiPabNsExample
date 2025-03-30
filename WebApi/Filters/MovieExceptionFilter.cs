using ApplicationCore.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Filters;

public class MovieExceptionFilter : ExceptionFilterAttribute
{
    public override Task OnExceptionAsync(ExceptionContext context)
    {
        if (context.Exception is MovieNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new
            {
                title = context.Exception.Message
            });
            context.ExceptionHandled = true;
        }
        return Task.CompletedTask;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is MovieNotFoundException)
        {
            context.Result = new NotFoundObjectResult(new
            {
                title = context.Exception.Message
            });
            context.ExceptionHandled = true;
        }
    }
}