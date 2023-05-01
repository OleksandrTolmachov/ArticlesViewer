using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetIfNameInUseQuery(string UserName) : IRequest<bool>;