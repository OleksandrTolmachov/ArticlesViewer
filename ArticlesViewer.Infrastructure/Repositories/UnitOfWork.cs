using ArticlesViewer.Application;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using Entities;

namespace ArticlesViewer.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context, IRepository<Article> articles,
        IRepository<TopicTag> topicTags, IArticleUserRepository articlesUsers)
    {
        _context = context;
        Articles = articles;
        TopicTags = topicTags;
        ArticlesUsers = articlesUsers;
    }

    public IRepository<Article> Articles { get; }
    public IRepository<TopicTag> TopicTags { get; }
    public IArticleUserRepository ArticlesUsers { get; }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}