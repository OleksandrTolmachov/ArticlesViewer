using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetArticleImageHandler : IRequestHandler<GetArticleImageQuery, IBlobObject>
{
    private readonly IBlobRepository _blobRepository;

    public GetArticleImageHandler(IBlobRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }

    public async Task<IBlobObject> Handle(GetArticleImageQuery request, CancellationToken cancellationToken)
    {
        var blobObj = await _blobRepository.GetBlobFileAsync(request.Id ?? "article-default", ContainerType.Images);

        return blobObj!;
    }
}
