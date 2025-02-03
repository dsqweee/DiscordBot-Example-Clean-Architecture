namespace DiscordBot.Core.Entities;

// DataBase entity
public class User
{
    public ulong Id { get; set; } // Discord User Id
    public ulong Money { get; set; }

    
    public ulong GuildId { get; set; } // Discord Guild Id
    public Guild Guild { get; set; } // One-To-Many relationship
}
