using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using AutoMapper;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetAllArticlesHandler : IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetAllArticlesHandler(IUnitOfWork unitOfWork,
        IMediator mediator,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ArticleResponse>> Handle(GetAllArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var articles = await _unitOfWork.Articles.GetAllAsync();
        var articleResponses = _mapper.Map<IEnumerable<ArticleResponse>>(articles);

        foreach (var article in articleResponses)
            article.Content = await _mediator.Send
                (new GetArticleTextQuery(article.Id.ToString()), cancellationToken);
        
        return articleResponses;
    }
}
