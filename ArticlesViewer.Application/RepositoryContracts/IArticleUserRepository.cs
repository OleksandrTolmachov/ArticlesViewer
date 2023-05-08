using ArticlesViewer.Domain;

namespace ArticlesViewer.Application
{
    public interface IArticleUserRepository
    {
        Task CreateAsync(ArticleUserHistory value);
        Task DeleteAsync(Guid articleId, Guid userId);
        Task<bool> ExistsAsync(Guid articleId, Guid userId);
        Task<IEnumerable<ArticleUserHistory>> GetAllAsync(Guid userId);
        Task<long> GetArticleViewCountAsync(Guid id);
        Task<IEnumerable<ArticleUserHistory>> GetArticleViewsAsync(Guid articleId);
        Task SaveAsync();
        Task UpdateAsync(ArticleUserHistory value);
    }
}