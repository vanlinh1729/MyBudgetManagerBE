using System.Net;
using System.Text.Json;
using FluentValidation;
using MyBudgetManager.Application.Common.Exceptions;

namespace MyBudgetManager.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

   private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
   {
       var response = context.Response;
       response.ContentType = "application/json";
   
       HttpStatusCode status;
       string message = exception.Message;
       object? errors = null;
   
       switch (exception)
       {
           // 🔹 Handle FluentValidation
           case ValidationException validationEx:
               status = HttpStatusCode.BadRequest;
               message = "Validation failed";
               errors = validationEx.Errors.Select(e => new
               {
                   field = e.PropertyName,
                   error = e.ErrorMessage
               });
               break;
   
           // 🔹 Các loại exception tùy chỉnh của cậu
           case BadRequestException:
               status = HttpStatusCode.BadRequest;
               break;
           case NotFoundException:
               status = HttpStatusCode.NotFound;
               break;
           case ConflictException:
               status = HttpStatusCode.Conflict;
               break;
           case UnauthorizedException:
               status = HttpStatusCode.Unauthorized;
               break;
   
           // 🔹 Default fallback
           default:
               status = HttpStatusCode.InternalServerError;
               message = "Internal Server Error";
               break;
       }
   
       var result = JsonSerializer.Serialize(new
       {
           success = false,
           status = (int)status,
           message,
           errors // nếu null sẽ bị bỏ qua
       });
   
       response.StatusCode = (int)status;
       await response.WriteAsync(result);
   }
}