using ArticlesViewer.Application.Commands;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}
