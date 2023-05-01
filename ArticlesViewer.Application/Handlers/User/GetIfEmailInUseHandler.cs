using MediatR;
using Microsoft.AspNetCore.Identity;
using ArticlesViewer.Domain;
using ArticlesViewer.Application.Queries;

namespace ArticlesViewer.Application.Handlers;

public class GetIfEmailInUseHandler : IRequestHandler<GetIfEmailInUseQuery, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetIfEmailInUseHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(GetIfEmailInUseQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        return user is not null;
    }
}
