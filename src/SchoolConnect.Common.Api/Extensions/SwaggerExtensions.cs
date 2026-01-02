using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace SchoolConnect.Common.Api.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerDocumentation(
        this IServiceCollection services,
        string title,
        string version = "v1",
        string? description = null)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc(version, new OpenApiInfo
            {
                Title = title,
                Version = version,
                Description = description ?? $"API documentation for {title}"
            });

            // Add correlation ID header
            options.AddSecurityDefinition("CorrelationId", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.ApiKey,
                In = ParameterLocation.Header,
                Name = "X-Correlation-ID",
                Description = "Optional correlation ID for request tracking"
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerDocumentation(
        this IApplicationBuilder app,
        string version = "v1")
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            options.RoutePrefix = string.Empty; // Serve Swagger UI at root
        });

        return app;
    }
}
