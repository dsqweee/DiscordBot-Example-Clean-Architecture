using Discord.Commands;
using Discord;

namespace DiscordBot.Application.Events;

public class CommandExecutedEvent
{
    public async Task OnCommandExecuted(Optional<CommandInfo> _, ICommandContext context, IResult result)
    {
        if (!result.IsSuccess)
        {
            await context.Channel.SendMessageAsync(result.ErrorReason);
        }
    }
}
