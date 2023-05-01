using MediatR;
using Microsoft.AspNetCore.Identity;
using ArticlesViewer.Domain;
using ArticlesViewer.Application.Queries;

namespace ArticlesViewer.Application.Handlers;

public class GetIfNameInUseHandler : IRequestHandler<GetIfNameInUseQuery, bool>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GetIfNameInUseHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(GetIfNameInUseQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        return user is not null;
    }
}