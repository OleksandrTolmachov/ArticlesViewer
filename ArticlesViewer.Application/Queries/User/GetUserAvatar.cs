using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetUserAvatarQuery(string Id) : IRequest<IBlobObject>;
