using DiscordBot.Core.Interfaces;
using Serilog;

namespace DiscordBot.Infrastructure.Logging;

// Repository for logging the bot data and other tasks
public class Logger<T> : ILogger<T>
{
    private readonly ILogger _logger;

    public Logger(ILogger logger)
    {
        _logger = logger;
    }

    public void LogInformation(string message, params object[] args)
    {
        _logger.Information(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.Warning(message, args);
    }

    public void LogError(Exception ex, string message, params object[] args)
    {
        _logger.Error(ex, message, args);
    }

    public void LogCritical(Exception ex, string message, params object[] args)
    {
        _logger.Fatal(ex, message, args);
    }
}
