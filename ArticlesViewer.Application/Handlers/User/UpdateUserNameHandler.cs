using ArticlesViewer.Application.Commands;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class UpdateUserNameHandler : INotificationHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;

    public UpdateUserNameHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.Id);
        if (notification.UserName is not null && user is not null)
        {
            user.UserName = notification.UserName;
            user.NormalizedUserName = notification.UserName?.ToUpperInvariant();
            await _userManager.UpdateAsync(user);
        }
    }
}
