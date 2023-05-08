using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using ArticlesViewer.UI.Policies.Requirements;
using ArticlesViewer.UI.Filters;

namespace ArticlesViewer.UI.Controllers;

[Route("{controller}/{action}")]
public class ArticleHandlerController : Controller
{
    private readonly IMediator _mediator;
    private readonly IAuthorizationService _authorizationService;

    public ArticleHandlerController(IMediator mediator, IAuthorizationService authorizationService)
    {
        _mediator = mediator;
        _authorizationService = authorizationService;
    }

    [HttpGet]
    public IActionResult CreateArticle()
    {
        return View();
    }

    [HttpPost]
    [TypeFilter(typeof(ModelValidationActionFilter))]
    public async Task<IActionResult> CreateArticle(CreateArticleCommand createRequest)
    {
        createRequest.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        await _mediator.Send(createRequest);

        return RedirectToAction("Index", "ArticleViewer");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteArticle(DeleteArticleCommand deleteCommand)
    {
        var result = await _authorizationService.AuthorizeAsync
            (User, deleteCommand, new AllowDeleteArticleRequirement());

        if (!result.Succeeded) return Forbid();

        await _mediator.Send(deleteCommand);
        return RedirectToAction("Index", "ArticleViewer");
    }

    [HttpGet]
    public async Task<IActionResult> EditArticle(GetArticleQuery getQuery)
    {
        return View(await _mediator.Send(getQuery));
    }
}

