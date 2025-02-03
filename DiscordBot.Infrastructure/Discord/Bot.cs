using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Application.Events;
using DiscordBot.Infrastructure.Services;

namespace DiscordBot.Infrastructure.Discord;

public class Bot
{
    private readonly MessageReceivedEvent _messageReceivedEvent;
    private readonly CommandExecutedEvent _commandExecutedEvent;
    private readonly CommandLoaderService _commandLoader;
    private readonly DiscordShardedClient _client;
    private readonly CommandService _commandService;
    private readonly LoggingEvent _loggingEvent;

    public Bot(LoggingEvent loggingEvent,
               DiscordShardedClient client,
               CommandService commandService,
               CommandLoaderService commandLoader,
               MessageReceivedEvent messageReceivedEvent,
               CommandExecutedEvent commandExecutedEvent)
    {
        _client = client;
        _loggingEvent = loggingEvent;
        _commandLoader = commandLoader;
        _commandService = commandService;
        _messageReceivedEvent = messageReceivedEvent;
        _commandExecutedEvent = commandExecutedEvent;
    }

    public async Task StartAsync(string token)
    {
        _client.Log += _loggingEvent.OnLog;
        _commandService.Log += _loggingEvent.OnLog;
        _client.MessageReceived += _messageReceivedEvent.OnMessageReceived;
        _commandService.CommandExecuted += _commandExecutedEvent.OnCommandExecuted;
        
        await _commandLoader.LoadCommandsAsync();

        await _client.LoginAsync(TokenType.Bot, token);
        await _client.StartAsync();
    }

    public async Task StopAsync()
    {
        await _client.LogoutAsync();
        await _client.StopAsync();

        _commandService.CommandExecuted -= _commandExecutedEvent.OnCommandExecuted;
        _client.MessageReceived -= _messageReceivedEvent.OnMessageReceived;
        _commandService.Log -= _loggingEvent.OnLog;
        _client.Log -= _loggingEvent.OnLog;
    }
}
