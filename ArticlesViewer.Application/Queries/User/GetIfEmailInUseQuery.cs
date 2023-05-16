using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetIfEmailInUseQuery(string Email) : IRequest<bool>;
