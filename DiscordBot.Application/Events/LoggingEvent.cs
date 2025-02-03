using Discord;
using DiscordBot.Core.Interfaces;

namespace DiscordBot.Application.Events;

public class LoggingEvent
{
    private readonly ILogger<LoggingEvent> _logger;

    public LoggingEvent(ILogger<LoggingEvent> logger)
    {
        _logger = logger;
    }

    public Task OnLog(LogMessage log)
    {
        switch (log.Severity)
        {
            case LogSeverity.Verbose:
            case LogSeverity.Info:
                _logger.LogInformation(log.Message);
                break;
            case LogSeverity.Warning:
                _logger.LogWarning(log.Message);
                break;
            case LogSeverity.Error:
                _logger.LogError(log.Exception, log.Message);
                break;
            case LogSeverity.Critical:
                _logger.LogCritical(log.Exception, log.Message);
                break;
        }

        return Task.CompletedTask;
    }
} 
