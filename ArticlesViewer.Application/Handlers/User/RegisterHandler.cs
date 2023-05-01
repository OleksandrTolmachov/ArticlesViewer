using MediatR;
using ArticlesViewer.Domain;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using ArticlesViewer.Application.Commands;

namespace ArticlesViewer.Application.Handlers;

public class RegisterHandler : IRequestHandler<RegisterCommand, IdentityResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public RegisterHandler(SignInManager<ApplicationUser> signInManager,
        IMapper mapper, UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IdentityResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<ApplicationUser>(request);
        var regResult = await _userManager.CreateAsync(user, request.Password);
        if (regResult.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
        }
        return regResult;
    }
}
