using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using Entities;

namespace ArticlesViewer.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;

    public UnitOfWork(AppDbContext context, IRepository<Article> articles)
    {
        this.context = context;
        Articles = articles;
    }
    public IRepository<Article> Articles { get; }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}