using Microsoft.AspNetCore.Http;

namespace SchoolConnect.Common.Infrastructure.Logging;

public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-ID";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = GetCorrelationId(context);
        context.Items[CorrelationIdHeader] = correlationId;
        context.Response.Headers.Append(CorrelationIdHeader, correlationId);

        await _next(context);
    }

    private static string GetCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
        {
            return correlationId.ToString();
        }

        return Guid.NewGuid().ToString();
    }
}
