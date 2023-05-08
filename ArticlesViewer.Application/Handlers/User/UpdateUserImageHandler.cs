using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Handlers;

public class UpdateUserImageHandler : INotificationHandler<UpdateUserCommand>
{
    private readonly IBlobRepository _blobRepository;
    private readonly UserManager<User> _userManager;

    public UpdateUserImageHandler(IBlobRepository blobRepository,
        UserManager<User> userManager)
    {
        _blobRepository = blobRepository;
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand notification, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(notification.Id);
        var image = notification.Image;
        if (image is not null)
        {
            var oldImageId = user.ImageId?.ToString();
            var imageId = Guid.NewGuid().ToString();
            await _blobRepository.UploadBlobFileAsync(image, imageId);
            user.ImageId = imageId;

            if (oldImageId is not null)
                await _blobRepository.DeleteBlobFileAsync(oldImageId);
        }
    }
}
