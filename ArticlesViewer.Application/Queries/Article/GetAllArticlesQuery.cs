using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetAllArticlesQuery : IRequest<IEnumerable<ArticleResponse>>;

public record GetSanitizedHtmlQuery(string Html) : IRequest<string>;
