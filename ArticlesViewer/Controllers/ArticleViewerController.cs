using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Domain;

namespace ArticlesR.Controllers;

[Route("{controller=ArticleViewer}/{action=Index}")]
public class ArticleViewerController : Controller
{
    private readonly IMediator _mediator;

    public ArticleViewerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var articles = await _mediator.Send(new GetAllArticlesQuery());
        return View(articles);
    }

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> ViewArticle(string id)
    {
        var article = await _mediator.Send(new GetArticleQuery(id));
        return View(article);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ArticleImage(string id)
    {
        BlobObject image = await _mediator.Send(new GetArticleImageQuery(id));
        return Ok(image.File);
    }
}
