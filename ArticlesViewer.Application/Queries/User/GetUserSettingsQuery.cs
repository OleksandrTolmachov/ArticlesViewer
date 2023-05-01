using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetUserSettingsQuery(string Id) : IRequest<UserUpdateResponse>;
