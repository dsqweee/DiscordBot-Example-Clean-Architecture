namespace DiscordBot.Core.Entities;

// DataBase entity
public class Guild
{
    public ulong Id { get; set; } // Discord Guild Id
    public ICollection<User> Users { get; set; } = new List<User>(); // Many-To-One  relationship
}
