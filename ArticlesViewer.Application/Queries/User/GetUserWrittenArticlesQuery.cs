using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetUserWrittenArticlesQuery(Guid Id) : IRequest<IEnumerable<ArticleResponse>>;
