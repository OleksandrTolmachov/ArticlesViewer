using ArticlesViewer.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ArticlesViewer.Application.Commands;

public record UpdateUserCommand : IRequest<UserResponse>, INotification
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public IFormFile? Image { get; init; }
}
