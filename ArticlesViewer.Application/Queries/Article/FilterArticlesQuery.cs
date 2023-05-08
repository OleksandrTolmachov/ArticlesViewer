using ArticlesViewer.Application.DTO;
using ArticlesViewer.Domain;
using ArticlesViewer.Domain.Enums;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record FilterArticlesQuery(TopicTags? Tag, Order Order = Order.Top, string TitleContains = "") 
    : IRequest<IEnumerable<ArticleResponse>>;

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
        var articles = await _mediator.Send(new GetAllArticlesQuery());
        foreach (var filterHandler in _filterHandlers)
            articles = filterHandler.Invoke(request, articles);

        return articles;
    }
}

public delegate IEnumerable<ArticleResponse> FilterHandler(FilterArticlesQuery request, IEnumerable<ArticleResponse> articles);

