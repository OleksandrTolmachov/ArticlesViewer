using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.Dtos;
using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Domain;
using ArticlesViewer.Infrastructure;
using ArticlesViewer.UI.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.UI.Policies.Handlers;

public class AllowDeleteArticleHandler : AuthorizationHandler<AllowDeleteArticleRequirement, DeleteArticleCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly IUnitOfWork _unitOfWork;

    public AllowDeleteArticleHandler(UserManager<User> userManager, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _unitOfWork = unitOfWork;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AllowDeleteArticleRequirement requirement, DeleteArticleCommand resource)
    {
        if (context.User.IsInRole(Roles.Admin.ToString()))
        {
            context.Succeed(requirement);
        }

        var article = await _unitOfWork.Articles.GetByIdAsync(resource.Id);
        var id = _userManager.GetUserId(context.User);

        if (id is not null && article is not null && Guid.Parse(id) == article.UserId)
        {
            context.Succeed(requirement);
        }
    }
}
