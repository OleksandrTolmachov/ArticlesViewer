using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetArticleQuery(string Id) : IRequest<ArticleResponse>;
