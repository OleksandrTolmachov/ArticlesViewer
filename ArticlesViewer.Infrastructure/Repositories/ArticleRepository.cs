using ArticlesViewer.Domain;
using Entities;
using Microsoft.EntityFrameworkCore;

namespace ArticlesViewer.Infrastructure;

public class ArticleRepository : Repository<Article>
{
    public ArticleRepository(AppDbContext context) : base(context)
    { }

    public override async Task<IEnumerable<Article>> GetAllAsync()
    {
        return await _table.Include(article => article.TopicTag).ToListAsync();
    }

    public override async Task<Article?> GetByIdAsync(Guid id)
    {
        return await _table.Include(article => article.TopicTag)
            .FirstOrDefaultAsync(article => article.Id == id);
    }
}
