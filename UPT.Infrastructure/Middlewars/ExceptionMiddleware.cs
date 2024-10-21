using FluentValidation;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace UPT.Infrastructure.Middlewars;

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
        catch (Exception ex)
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

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        string result;

        if (exception is BackendException)
        {
            code = HttpStatusCode.BadRequest;
            result = JsonSerializer.Serialize(new { status = 410, message = exception.Message });
        }
        else
        {
            result = JsonSerializer.Serialize(new { status = 500, message = exception.Message });
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsync(result);
    }
}




/* Структура ошибок
 {
  "status": 400,
  "message": "Invalid input",
  "errors": [
    {
      "field": "email",
      "error": "Email is required"
    },
    {
      "field": "password",
      "error": "Password must be at least 8 characters"
    }
  ]
}
 */