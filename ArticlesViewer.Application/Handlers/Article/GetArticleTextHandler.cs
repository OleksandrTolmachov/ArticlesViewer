using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetArticleTextHandler : IRequestHandler<GetArticleTextQuery, string>
{
    private readonly IBlobRepository _blobRepository;
    private readonly IMediator _mediator;

    public GetArticleTextHandler(IBlobRepository blobRepository, IMediator mediator)
    {
        _blobRepository = blobRepository;
        _mediator = mediator;
    }

    public async Task<string> Handle(GetArticleTextQuery request, CancellationToken cancellationToken)
    {
        var blobObj = await _blobRepository.GetBlobFileAsync(request.Id, ContainerType.ArticlesContent);
        if (blobObj is null || blobObj.File is null) return string.Empty;

        using var reader = new StreamReader(blobObj.File);
        string content = reader.ReadToEnd();

        content = await _mediator.Send(new GetSanitizedHtmlQuery(content), cancellationToken);
        return content;
    }
}



