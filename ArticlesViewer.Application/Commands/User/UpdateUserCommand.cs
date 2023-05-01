using ArticlesViewer.Application.DTO;
using MediatR;

namespace ArticlesViewer.Application.Commands;

public record UpdateUserCommand(UserUpdateResponse UserRequest) : IRequest, INotification;
