using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class FilterArticlesHandler : IRequestHandler<FilterArticlesQuery, IEnumerable<ArticleResponse>>
{
    private readonly IMediator _mediator;
    private readonly IEnumerable<FilterHandler> _filterHandlers;

    public FilterArticlesHandler(IMediator mediator, IEnumerable<FilterHandler> filterHandlers)
    {
        _mediator = mediator; ;
        _filterHandlers = filterHandlers;
    }

    public async Task<IEnumerable<ArticleResponse>> Handle(FilterArticlesQuery request, CancellationToken cancellationToken)
    {
        var articles = await _mediator.Send(new GetAllArticlesQuery(), cancellationToken);
        foreach (var filterHandler in _filterHandlers)
            articles = filterHandler.Invoke(request, articles);

        return articles;
    }
}

public delegate IEnumerable<ArticleResponse> FilterHandler(FilterArticlesQuery request, IEnumerable<ArticleResponse> articles);


