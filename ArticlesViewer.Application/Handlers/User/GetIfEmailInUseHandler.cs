using ArticlesViewer.Application.Queries;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class GetIfEmailInUseHandler : IRequestHandler<GetIfEmailInUseQuery, bool>
{
    private readonly UserManager<User> _userManager;

    public GetIfEmailInUseHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(GetIfEmailInUseQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        return user is not null;
    }
}
