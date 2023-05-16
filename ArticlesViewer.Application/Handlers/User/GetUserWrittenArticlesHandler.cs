using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using AutoMapper;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetUserWrittenArticlesHandler : IRequestHandler<GetUserWrittenArticlesQuery, IEnumerable<ArticleResponse>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<User> _userRepository;

    public GetUserWrittenArticlesHandler(IMapper mapper, IRepository<User> userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<ArticleResponse>> Handle(GetUserWrittenArticlesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        return _mapper.Map<IEnumerable<ArticleResponse>>(user?.WrittenArticles);
    }
}