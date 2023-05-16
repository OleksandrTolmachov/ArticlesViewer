using ArticlesViewer.Application;
using ArticlesViewer.Domain;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesViewer.Infrastructure;

public class ArticleUserRepository : IArticleUserRepository
{
    private readonly AppDbContext _context;

    public ArticleUserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ArticleUserHistory>> GetArticleViewsAsync(Guid articleId)
    {
        return await _context.ArticleUserHistories.Include(history => history.Article)
            .Include(history => history.User)
            .Where(history => history.ArticleId == articleId).ToListAsync();
    }

    public async Task<long> GetArticleViewCountAsync(Guid id)
    {
        return await _context.ArticleUserHistories
            .Where(history => history.ArticleId == id)
            .CountAsync();
    }

    public async Task<bool> ExistsAsync(Guid articleId, Guid userId)
    {
        return (await _context.ArticleUserHistories.AsNoTracking()
            .FirstOrDefaultAsync(history => history.UserId == userId &&
                history.ArticleId == articleId)) != null;
    }

    public async Task<IEnumerable<ArticleUserHistory>> GetAllAsync(Guid userId)
    {
        var a = await _context.ArticleUserHistories.Include(history => history.Article)
            .Where(history => history.UserId == userId)
            .ToListAsync();
        return a;
    }

    public async Task CreateAsync(ArticleUserHistory value)
    {
        await _context.AddAsync(value);
    }

    public Task UpdateAsync(ArticleUserHistory value)
    {
        return Task.FromResult(_context.Update(value));
    }

    public async Task DeleteAsync(Guid articleId, Guid userId)
    {
        var history = await _context.ArticleUserHistories.FindAsync(articleId, userId);

        if (history is not null)
            _context.ArticleUserHistories.Remove(history);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}