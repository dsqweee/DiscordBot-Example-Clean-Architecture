using DiscordBot.Infrastructure.Discord;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DiscordBot.Presentation;

public class BotHostedService : IHostedService
{
    private readonly Bot _bot;
    private readonly IConfiguration _configuration;

    public BotHostedService(Bot bot, IConfiguration configuration)
    {
        _bot = bot;
        _configuration = configuration;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _bot.StartAsync(_configuration["Discord:Token"]);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _bot.StopAsync();
    }
}
