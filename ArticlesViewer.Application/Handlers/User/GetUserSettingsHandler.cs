using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class GetUserSettingsHandler : IRequestHandler<GetUserSettingsQuery, UserUpdateResponse>
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetUserSettingsHandler(IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<UserUpdateResponse> Handle(GetUserSettingsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        return _mapper.Map<UserUpdateResponse>(user);
    }
}
