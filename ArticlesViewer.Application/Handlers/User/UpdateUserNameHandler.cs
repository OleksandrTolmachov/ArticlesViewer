using ArticlesViewer.Application.Commands;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class UpdateUserNameHandler : INotificationHandler<UpdateUserCommand>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UpdateUserNameHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.UserRequest.Id);
        if (notification.UserRequest.UserName is not null && user is not null)
        {
            user.UserName = notification.UserRequest.UserName;
            user.NormalizedUserName = notification.UserRequest.UserName?.ToUpperInvariant();
            await _userManager.UpdateAsync(user);
        }
    }
}
