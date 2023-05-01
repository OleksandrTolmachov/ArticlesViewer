using MediatR;
using System.Security.Claims;

namespace ArticlesViewer.Application.Queries;

public record GetIfEmailInUseQuery(string Email) : IRequest<bool>;
