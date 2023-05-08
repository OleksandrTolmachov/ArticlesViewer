using ArticlesViewer.Domain;

namespace ArticlesViewer.Application.RepositoryContracts;

public interface IUnitOfWork
{
    IRepository<Article> Articles { get; }
    IRepository<TopicTag> TopicTags { get; }
    IArticleUserRepository ArticlesUsers { get; }

    Task SaveChangesAsync();
}
