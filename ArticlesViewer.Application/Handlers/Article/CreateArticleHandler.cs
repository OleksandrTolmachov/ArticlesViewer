using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Commands.Articles;

public class CreateArticleHandler : IRequestHandler<CreateArticleCommand>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IBlobRepository _blobRepository;

    public CreateArticleHandler(IMapper mapper, IUnitOfWork unitOfWork,
        UserManager<User> userManager, IBlobRepository blobRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _blobRepository = blobRepository;
    }

    public async Task Handle(CreateArticleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user is null) throw new ArgumentException(nameof(user));
        var article = _mapper.Map<Article>(request);

        article.User = user;
        article.PublicationDate = DateTime.Now;
        article.Id = Guid.NewGuid();

        if (request.Image is not null)
            await _blobRepository.UploadBlobFileAsync
                (request.Image, article.Id.ToString());

        request.TopicTag.Id = Guid.NewGuid();
        await _unitOfWork.TopicTags.CreateAsync(request.TopicTag);
        await _unitOfWork.Articles.CreateAsync(article);
        await _unitOfWork.SaveChangesAsync();

        await _blobRepository.UploadBlobTextAsync(request.Content,
            article.Id.ToString(),
            ContainerType.ArticlesContent);
    }
}

