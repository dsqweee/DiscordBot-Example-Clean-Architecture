using Discord.Commands;
using Discord.WebSocket;
using Discord;

namespace DiscordBot.Infrastructure.Configuration;

public class DiscordConfig
{
    private static readonly int[] shardIds = Enumerable.Range(0, 3).ToArray();

    public static readonly DiscordSocketConfig shardConfig = new DiscordSocketConfig
    {
        LogLevel = LogSeverity.Verbose,
        MessageCacheSize = 128,
        AlwaysDownloadUsers = true,
        TotalShards = shardIds.Length,
        GatewayIntents = GatewayIntents.All // Required
    };

    public static readonly CommandServiceConfig commandConfig = new CommandServiceConfig
    {
        LogLevel = LogSeverity.Verbose,
        DefaultRunMode = RunMode.Async,
    };
}
