using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.UI.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArticlesViewer.UI.Controllers;

[Route("{controller}/{action}")]
public class ArticleHandlerController : Controller
{
    private readonly IMediator _mediator;

    public ArticleHandlerController(IMediator mediator)
    {
        _mediator = mediator;
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
    [TypeFilter(typeof(DeletePermissionAuthorizationFilter))]
    public async Task<IActionResult> DeleteArticle([FromForm] DeleteArticleCommand deleteCommand)
    {
        await _mediator.Send(deleteCommand);
        return RedirectToAction("Index", "ArticleViewer");
    }

    [HttpGet]
    public async Task<IActionResult> EditArticle(GetArticleQuery getQuery)
    {
        return View(await _mediator.Send(getQuery));
    }
}

