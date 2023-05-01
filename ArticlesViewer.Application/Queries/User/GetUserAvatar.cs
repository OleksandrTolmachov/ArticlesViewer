using ArticlesViewer.Domain;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetUserAvatarQuery(string Id) : IRequest<BlobObject>;
