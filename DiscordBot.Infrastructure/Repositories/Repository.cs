using DiscordBot.Core.Interfaces;

namespace DiscordBot.Infrastructure.Repositories;

// Repository for working with the database
public class Repository<T> : IRepository<T> where T : class
{
    private readonly DiscordBotDbContext _context;

    public Repository(DiscordBotDbContext context)
    {
        _context = context;
    }

    public async Task<T> GetByIdAsync(ulong id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
