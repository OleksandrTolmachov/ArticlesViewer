using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Domain;
using System.Security.Claims;

namespace ArticlesViewer.UI.Controllers;

[Route("{controller=ArticleViewer}/{action=Index}")]
[AllowAnonymous]
public class ArticleViewerController : Controller
{
    private readonly IMediator _mediator;

    public ArticleViewerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> Index()
    {
        var articles = await _mediator.Send(new GetAllArticlesQuery());
        return View(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ViewArticle(GetArticleQuery query)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        if(userId is not null) query.UserId = Guid.Parse(userId.Value); 
        var article = await _mediator.Send(query);
        return View(article);
    }

    [HttpGet]
    public async Task<IActionResult> ArticleImage(GetArticleImageQuery query)
    {
        BlobObject image = await _mediator.Send(query);
        return File("/icons/article.png", "png");
    }
}
