using Discord.Commands;
using Discord.WebSocket;
using DiscordBot.Application.Events;
using DiscordBot.Core.Entities;
using DiscordBot.Core.Interfaces;
using DiscordBot.Infrastructure;
using DiscordBot.Infrastructure.Discord;
using DiscordBot.Infrastructure.Logging;
using DiscordBot.Infrastructure.Repositories;
using DiscordBot.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DiscordBot.Presentation;

public class Program
{
    public static async Task Main()
    {
        var host = CreateHostBuilder().Build();
        await host.RunAsync();
    }

    public static IHostBuilder CreateHostBuilder()
    {
        var host = Host.CreateDefaultBuilder()
            .UseSerilog((context, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                               .Enrich.FromLogContext()
                               .WriteTo.Console()
                               .WriteTo.File(
                                    path: "logs/log.txt",
                                    rollingInterval: RollingInterval.Day,
                                    retainedFileCountLimit: 7);
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                config.SetBasePath(AppContext.BaseDirectory);
                config.AddJsonFile("appsettings.json", optional:false, reloadOnChange: true);
                config.AddEnvironmentVariables();
            })
            .ConfigureServices((context, services) =>
            {
                string dbConnectionString = context.Configuration.GetConnectionString("SQLiteConnection");
                services.AddDbContext<DiscordBotDbContext>(options =>
                    options.UseSqlite(dbConnectionString));

                services.AddTransient(typeof(ILogger<>), typeof(Logger<>));

                services.AddScoped<IRepository<User>, Repository<User>>();
                services.AddScoped<IRepository<Guild>, Repository<Guild>>();

                services.AddSingleton<Bot>();
                services.AddSingleton(new CommandService(Infrastructure.Configuration.DiscordConfig.commandConfig));
                services.AddSingleton(new DiscordShardedClient(Infrastructure.Configuration.DiscordConfig.shardConfig));
                
                services.AddSingleton<CommandLoaderService>();
                
                services.AddSingleton<LoggingEvent>();
                services.AddSingleton<CommandExecutedEvent>();
                services.AddSingleton<MessageReceivedEvent>();

                services.AddHostedService<BotHostedService>();
            });

        return host;
    }
}