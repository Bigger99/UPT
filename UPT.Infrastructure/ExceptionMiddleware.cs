using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace UPT.Infrastructure;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, ValidationException exception)
    {
        context.Response.ContentType = "application/json";
        var exceptionMessage = exception.Message;
        context.Response.StatusCode = int.Parse(exception.Errors.FirstOrDefault()?.ErrorCode ?? "422");
        return context.Response.WriteAsync(exceptionMessage);
    }
}