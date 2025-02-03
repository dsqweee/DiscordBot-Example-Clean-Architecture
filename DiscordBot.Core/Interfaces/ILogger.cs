namespace DiscordBot.Core.Interfaces;

// Interface for logging the bot data and other tasks
public interface ILogger<T>
{
    void LogCritical(Exception ex, string message, params object[] args);
    void LogError(Exception ex, string message, params object[] args);
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}