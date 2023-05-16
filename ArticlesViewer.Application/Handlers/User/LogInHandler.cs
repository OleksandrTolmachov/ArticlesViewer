using ArticlesViewer.Application.Commands;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class LogInHandler : IRequestHandler<LogInCommand, SignInResult>
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;

    public LogInHandler(SignInManager<User> signInManager, UserManager<User> userManager)
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
