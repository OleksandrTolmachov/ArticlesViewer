using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Queries;

public class GetUserAvatarHandler : IRequestHandler<GetUserAvatarQuery, IBlobObject>
{
    private readonly IBlobRepository _blobRepository;
    private readonly UserManager<User> _userManager;

    public GetUserAvatarHandler(IBlobRepository blobRepository, UserManager<User> userManager)
    {
        _blobRepository = blobRepository;
        _userManager = userManager;
    }

    public async Task<IBlobObject> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        var file = await _blobRepository.GetBlobFileAsync(user.ImageId ?? "user-default", ContainerType.Images);
        return file!;
    }
}