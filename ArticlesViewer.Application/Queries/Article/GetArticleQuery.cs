using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetArticleQuery(Guid Id) : IRequest<ArticleResponse>
{
    public Guid UserId { get; set; }
}
