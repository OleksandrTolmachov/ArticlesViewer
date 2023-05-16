using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetUserHistoryHandler : IRequestHandler<GetUserHistoryQuery, IEnumerable<ArticleUserHistory>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserHistoryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<ArticleUserHistory>> Handle(GetUserHistoryQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.ArticlesUsers.GetAllAsync(request.UserId);
    }
}
