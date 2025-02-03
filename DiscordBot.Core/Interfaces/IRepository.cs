namespace DiscordBot.Core.Interfaces;

// Interface for working with the database
public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(ulong id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
