using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace SchoolConnect.Common.Api.Extensions;

public static class EndpointExtensions
{
    public static RouteHandlerBuilder WithName(this RouteHandlerBuilder builder, string name)
    {
        return builder.WithName(name);
    }

    public static RouteHandlerBuilder WithTags(this RouteHandlerBuilder builder, params string[] tags)
    {
        return builder.WithTags(tags);
    }

    public static RouteHandlerBuilder Produces<T>(this RouteHandlerBuilder builder, int statusCode = StatusCodes.Status200OK)
    {
        return builder.Produces<T>(statusCode);
    }

    public static RouteHandlerBuilder ProducesValidationProblem(this RouteHandlerBuilder builder)
    {
        return builder.ProducesValidationProblem();
    }
}
