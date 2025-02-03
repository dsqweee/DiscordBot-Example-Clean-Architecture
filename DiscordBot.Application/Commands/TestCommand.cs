using Discord.Commands;
using DiscordBot.Core.Entities;
using DiscordBot.Core.Interfaces;

namespace DiscordBot.Application.Commands;

public class TestCommand : ModuleBase<ShardedCommandContext>
{
    private readonly ILogger<TestCommand> _logger;
    private readonly IRepository<Guild> _guildDb;

    public TestCommand(ILogger<TestCommand> logger, IRepository<Guild> guildDb)
    {
        _logger = logger;
        _guildDb = guildDb;
    }

    [Command("test")]
    public async Task Test()
    {
        var guild = await _guildDb.GetByIdAsync(Context.Guild.Id);

        await ReplyAsync($"{guild.Id}");
    }
}
