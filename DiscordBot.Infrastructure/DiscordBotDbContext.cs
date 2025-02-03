using DiscordBot.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace DiscordBot.Infrastructure;

public class DiscordBotDbContext : DbContext
{
    public DiscordBotDbContext(DbContextOptions<DiscordBotDbContext> options) : base(options) 
    { 
        base.Database.EnsureCreated(); 
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Guild> Guilds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
