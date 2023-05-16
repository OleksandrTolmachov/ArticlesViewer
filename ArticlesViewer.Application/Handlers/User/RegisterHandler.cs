using ArticlesViewer.Application.Commands;
using ArticlesViewer.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, IdentityResult>
{
    private readonly SignInManager<User> _signInManager;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public RegisterHandler(SignInManager<User> signInManager,
        IMapper mapper, UserManager<User> userManager)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(request);
        var regResult = await _userManager.CreateAsync(user, request.Password);
        if (regResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
        return regResult;
    }
}
