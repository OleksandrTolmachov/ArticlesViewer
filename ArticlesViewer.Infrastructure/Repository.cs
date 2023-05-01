using ArticlesViewer.Application.RepositoryContracts;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesViewer.Infrastructure;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _table;   

    public Repository(AppDbContext context)
    {
        _context = context;
        _table = _context.Set<T>();
    }

    public async Task CreateAsync(T value)
    {
        await _table.AddAsync(value);
    }

    public async Task DeleteAsync(object id)
    {
        var entity = await _table.FindAsync(id);
        if(entity is not null)
            _table.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _table.ToArrayAsync();
    }

    public async Task<T?> GetByIdAsync(object id)
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
