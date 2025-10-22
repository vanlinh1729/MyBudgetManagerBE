using System.Net;
using System.Text.Json;
using FluentValidation;
using MyBudgetManager.Application.Common.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace MyBudgetManager.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlingMiddleware(
        RequestDelegate next, 
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
            
            // 🔹 Xử lý các HTTP Status Code (401, 403, 404, 500, etc.)
            if (context.Response.StatusCode >= 400)
            {
                await HandleStatusCodeAsync(context);
            }
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
            // 🔹 JWT/Security Token Exceptions
            case SecurityTokenExpiredException:
                status = HttpStatusCode.Unauthorized;
                message = "Token has expired";
                break;
            case SecurityTokenInvalidIssuerException:
                status = HttpStatusCode.Unauthorized;
                message = "Token issuer is invalid";
                break;
            case SecurityTokenInvalidAudienceException:
                status = HttpStatusCode.Unauthorized;
                message = "Token audience is invalid";
                break;
            case SecurityTokenException:
                status = HttpStatusCode.Unauthorized;
                message = "Invalid security token";
                break;

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

            // 🔹 Custom Exceptions
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

            // 🔹 Default
            default:
                status = HttpStatusCode.InternalServerError;
                message = "Internal Server Error";
                break;
        }

        await WriteResponseAsync(response, status, message, errors);
    }

    // 🔹 QUAN TRỌNG: Xử lý các HTTP Status Code
    private async Task HandleStatusCodeAsync(HttpContext context)
    {
        var response = context.Response;
        response.ContentType = "application/json";

        var statusCode = (HttpStatusCode)context.Response.StatusCode;
        var message = GetDefaultMessageForStatusCode(statusCode);

        await WriteResponseAsync(response, statusCode, message, null);
    }

    private static string GetDefaultMessageForStatusCode(HttpStatusCode statusCode)
    {
        return statusCode switch
        {
            HttpStatusCode.Unauthorized => "Unauthorized access",
            HttpStatusCode.Forbidden => "Access forbidden",
            HttpStatusCode.NotFound => "Resource not found",
            HttpStatusCode.BadRequest => "Bad request",
            HttpStatusCode.InternalServerError => "Internal server error",
            HttpStatusCode.MethodNotAllowed => "Method not allowed",
            _ => "An error occurred"
        };
    }

    private static async Task WriteResponseAsync(
        HttpResponse response, 
        HttpStatusCode statusCode, 
        string message, 
        object? errors)
    {
        var result = JsonSerializer.Serialize(new
        {
            success = false,
            status = (int)statusCode,
            message,
            errors,
            timestamp = DateTime.UtcNow
        }, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        });

        response.StatusCode = (int)statusCode;
        await response.WriteAsync(result);
    }
}