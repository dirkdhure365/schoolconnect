using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace SchoolConnect.Common.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = context.Items["X-Correlation-ID"]?.ToString() ?? Guid.NewGuid().ToString();
        var stopwatch = Stopwatch.StartNew();

        _logger.LogInformation(
            "HTTP {Method} {Path} started. CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            correlationId);

        await _next(context);

        stopwatch.Stop();

        _logger.LogInformation(
            "HTTP {Method} {Path} completed in {ElapsedMilliseconds}ms with status code {StatusCode}. CorrelationId: {CorrelationId}",
            context.Request.Method,
            context.Request.Path,
            stopwatch.ElapsedMilliseconds,
            context.Response.StatusCode,
            correlationId);
    }
}
