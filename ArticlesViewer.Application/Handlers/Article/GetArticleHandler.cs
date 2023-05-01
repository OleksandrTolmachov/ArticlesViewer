using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain.Exceptions;
using AutoMapper;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetArticleHandler : IRequestHandler<GetArticleQuery, ArticleResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public GetArticleHandler(IUnitOfWork unitOfWork,
        IMediator mediator,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ArticleResponse> Handle(GetArticleQuery request,
        CancellationToken cancellationToken)
    {
        var article = await _unitOfWork.Articles.GetByIdAsync(request.Id.TryConvertThrowExceptionIfFail())
            ?? throw new ArticleNotFoundException($"Article {request.Id} is not found");
        var articleResponse = _mapper.Map<ArticleResponse>(article);
        articleResponse.Content = await _mediator.Send(new GetArticleTextQuery(article.Id.ToString()), cancellationToken);
        return articleResponse;
    }
}

