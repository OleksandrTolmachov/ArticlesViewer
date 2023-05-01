using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Commands.Articles;

public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBlobRepository _blobRepository;

    public DeleteArticleHandler(IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager, IBlobRepository blobRepository)
    {
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _blobRepository = blobRepository;
    }

    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var guid = request.Id.TryConvertThrowExceptionIfFail();

        var article = await _unitOfWork.Articles.GetByIdAsync(guid);
        await _blobRepository.DeleteBlobFileAsync(request.Id, ContainerType.ArticlesContent);
        if (article is not null)
        {
            await _unitOfWork.Articles.DeleteAsync(guid);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

