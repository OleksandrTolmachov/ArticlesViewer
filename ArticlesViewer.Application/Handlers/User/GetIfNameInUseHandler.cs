using ArticlesViewer.Application.Queries;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class GetIfNameInUseHandler : IRequestHandler<GetIfNameInUseQuery, bool>
{
    private readonly UserManager<User> _userManager;

    public GetIfNameInUseHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(GetIfNameInUseQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByNameAsync(request.UserName);
        return user is not null;
    }
}