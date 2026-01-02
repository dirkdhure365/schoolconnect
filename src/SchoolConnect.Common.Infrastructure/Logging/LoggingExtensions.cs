using Microsoft.Extensions.Logging;

namespace SchoolConnect.Common.Infrastructure.Logging;

public static class LoggingExtensions
{
    public static ILogger WithCorrelationId(this ILogger logger, string correlationId)
    {
        return logger;
    }

    public static IDisposable BeginPropertyScope(
        this ILogger logger,
        params (string Key, object Value)[] properties)
    {
        var dictionary = properties.ToDictionary(p => p.Key, p => p.Value);
        return logger.BeginScope(dictionary)!;
    }

    public static void LogWithContext(
        this ILogger logger,
        LogLevel logLevel,
        string message,
        params (string Key, object Value)[] properties)
    {
        using (logger.BeginPropertyScope(properties))
        {
            logger.Log(logLevel, message);
        }
    }
}
