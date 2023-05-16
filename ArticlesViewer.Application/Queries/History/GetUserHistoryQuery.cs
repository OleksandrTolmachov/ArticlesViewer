using ArticlesViewer.Domain;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetUserHistoryQuery(Guid UserId) : IRequest<IEnumerable<ArticleUserHistory>>;
