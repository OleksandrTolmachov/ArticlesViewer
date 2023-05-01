using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetArticleTextHandler : IRequestHandler<GetArticleTextQuery, string>
{
    private readonly IBlobRepository _blobRepository;

    public GetArticleTextHandler(IBlobRepository blobRepository)
    {
        _blobRepository = blobRepository;
    }

    public async Task<string> Handle(GetArticleTextQuery request, CancellationToken cancellationToken)
    {
        var blobObj = await _blobRepository.GetBlobFileAsync(request.Id, ContainerType.ArticlesContent);
        if (blobObj is null || blobObj.File is null) return string.Empty;

        using var reader = new StreamReader(blobObj.File);
        string content = reader.ReadToEnd();

        return content;
    }
}



