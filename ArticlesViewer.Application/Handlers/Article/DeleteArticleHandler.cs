using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Commands.Articles;

public class DeleteArticleHandler : IRequestHandler<DeleteArticleCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBlobRepository _blobRepository;

    public DeleteArticleHandler(IUnitOfWork unitOfWork, IBlobRepository blobRepository)
    {
        _unitOfWork = unitOfWork;
        _blobRepository = blobRepository;
    }

    public async Task Handle(DeleteArticleCommand request, CancellationToken cancellationToken)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(request.Id);
        await _blobRepository.DeleteBlobFileAsync(request.Id.ToString(), ContainerType.ArticlesContent);
        if (article is not null)
        {
            await _unitOfWork.Articles.DeleteAsync(request.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

