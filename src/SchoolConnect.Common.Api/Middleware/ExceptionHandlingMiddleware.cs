using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SchoolConnect.Common.Application.Exceptions;

namespace SchoolConnect.Common.Api.Middleware;

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
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message) = exception switch
        {
            NotFoundException => (HttpStatusCode.NotFound, exception.Message),
            ValidationException validationEx => (HttpStatusCode.BadRequest, JsonSerializer.Serialize(new
            {
                Message = validationEx.Message,
                Errors = validationEx.Errors
            })),
            ConflictException => (HttpStatusCode.Conflict, exception.Message),
            ForbiddenException => (HttpStatusCode.Forbidden, exception.Message),
            Application.Exceptions.ApplicationException => (HttpStatusCode.BadRequest, exception.Message),
            _ => (HttpStatusCode.InternalServerError, "An internal server error occurred.")
        };

        var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? Guid.NewGuid().ToString();

        _logger.LogError(exception,
            "Error occurred. CorrelationId: {CorrelationId}, StatusCode: {StatusCode}",
            correlationId, statusCode);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            CorrelationId = correlationId,
            Timestamp = DateTime.UtcNow
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
