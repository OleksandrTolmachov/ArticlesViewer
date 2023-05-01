using ArticlesViewer.Domain;

namespace ArticlesViewer.Application.RepositoryContracts;

public interface IUnitOfWork
{
    IRepository<Article> Articles { get; }
    Task SaveChangesAsync();
}
