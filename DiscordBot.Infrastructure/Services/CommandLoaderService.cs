using Discord.Commands;

namespace DiscordBot.Infrastructure.Services;

public class CommandLoaderService
{
    private const string pathCommandsBuild = $"{nameof(DiscordBot)}.{nameof(DiscordBot.Application)}";

    private readonly CommandService _commandService;
    private readonly IServiceProvider _service;

    public CommandLoaderService(CommandService commandService, IServiceProvider service)
    {
        _commandService = commandService;
        _service = service;
    }

    public async Task LoadCommandsAsync()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        var commandAssembly = assemblies.FirstOrDefault(x => x.FullName?.StartsWith(pathCommandsBuild) is true);

        if (commandAssembly == null)
            throw new DirectoryNotFoundException("При загрузке директории с командами, произошла ошибка!");

        var modules = await _commandService.AddModulesAsync(commandAssembly, _service);

        if (!modules.Any(module => module.Commands.Any()))
            throw new InvalidOperationException("Команды бота, не были загружены!");
    }
}
