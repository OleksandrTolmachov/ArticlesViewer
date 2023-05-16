using ArticlesViewer.Application.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesViewer.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _table;

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public virtual async Task CreateAsync(T value)
    {
        await _table.AddAsync(value);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _table.FindAsync(id);
        if (entity is not null)
            _table.Remove(entity);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _table.ToArrayAsync();
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _table.FindAsync(id);
    }

    public async Task UpdateAsync(T value)
    {
        await Task.FromResult(_table.Update(value));
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
