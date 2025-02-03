using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Core.Entities;
using DiscordBot.Core.Interfaces;

namespace DiscordBot.Application.Events;

public class MessageReceivedEvent
{
    private readonly DiscordShardedClient _client;
    private readonly IRepository<Guild> _guildDb;
    private readonly IServiceProvider _services;
    private readonly CommandService _commandService;

    public MessageReceivedEvent(IRepository<Guild> guildDb,
                                IServiceProvider services, 
                                DiscordShardedClient client, 
                                CommandService commandService)
    {
        _client = client;
        _guildDb = guildDb;
        _services = services;
        _commandService = commandService;
    }

    public async Task OnMessageReceived(SocketMessage message)
    {
        if (message.Author.IsBot)
            return;

        if (message is not SocketUserMessage userMessage)
            return;

        if (userMessage.Channel.ChannelType == Discord.ChannelType.DM)
            return;
        
        var context = new ShardedCommandContext(_client, userMessage);

        await GuildAvailability(context.Guild.Id);

        string prefix = "!";

        int argPos = 0;
        if (context.Message.HasStringPrefix(prefix, ref argPos))
            await _commandService.ExecuteAsync(context, argPos, _services);
    }

    private async Task GuildAvailability(ulong guildId)
    {
        var guild = await _guildDb.GetByIdAsync(guildId);

        if (guild == null)
        {
            guild = new Guild { Id = guildId };
            await _guildDb.AddAsync(guild);
        }
    }
}
