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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IBlobRepository _blobRepository;

    public CreateArticleHandler(IMapper mapper, IUnitOfWork unitOfWork,
        UserManager<ApplicationUser> userManager, IBlobRepository blobRepository)
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

        await _unitOfWork.Articles.CreateAsync(article);
        await _unitOfWork.SaveChangesAsync();

        await _blobRepository.UploadBlobTextAsync(request.Content,
            article.Id.ToString(),
            ContainerType.ArticlesContent);
    }
}

public static class StringToGuidExtensions
{
    public static Guid TryConvertThrowExceptionIfFail(this string id)
    {
        if (!Guid.TryParse(id, out Guid guid))
            throw new ArgumentException($"{nameof(id)} is not in guid format.");

        return guid;
    }
}

