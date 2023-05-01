using MediatR;
using Microsoft.AspNetCore.Identity;
using ArticlesViewer.Domain;
using ArticlesViewer.Application.Commands;

namespace ArticlesViewer.Application.Handlers;

public class LogInHandler : IRequestHandler<LogInCommand, SignInResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public LogInHandler(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<SignInResult> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        SignInResult signInResult = new();
        if (user is not null)
        {
            signInResult = await _signInManager.PasswordSignInAsync(user, request.Password,
                request.RememberMe, false);
        }
        return signInResult;
    }
}
