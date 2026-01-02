using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SchoolConnect.Common.Api.Middleware;

namespace SchoolConnect.Common.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonApiServices(this IServiceCollection services)
    {
        // Add common API services here
        services.AddHttpContextAccessor();

        return services;
    }

    public static IApplicationBuilder UseCommonMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<CorrelationIdMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        return app;
    }
}
