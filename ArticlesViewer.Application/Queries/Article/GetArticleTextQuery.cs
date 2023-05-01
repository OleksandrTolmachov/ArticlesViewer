using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetArticleTextQuery(string Id) : IRequest<string>;
