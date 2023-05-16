using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class AddUserArticleHistoryHandler : IRequestHandler<AddUserArticleHistoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddUserArticleHistoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddUserArticleHistoryCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty || request.UserId == null) return;

        if (await _unitOfWork.ArticlesUsers.ExistsAsync(request.ArticleId, request.UserId.Value))
        {
            await _unitOfWork.ArticlesUsers.UpdateAsync(new ArticleUserHistory()
            { ArticleId = request.ArticleId, UserId = request.UserId.Value, DateTime = DateTime.Now });
            await _unitOfWork.SaveChangesAsync();
            return;
        }

        await _unitOfWork.ArticlesUsers.CreateAsync(new ArticleUserHistory()
        { ArticleId = request.ArticleId, UserId = request.UserId.Value, DateTime = DateTime.Now });
        await _unitOfWork.SaveChangesAsync();
    }
}

