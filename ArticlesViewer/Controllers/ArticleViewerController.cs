﻿using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.RepositoryContracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> Index(FilterArticlesQuery query)
    {
        var articles = await _mediator.Send(query);
        ViewBag.FilterContext = query;
        return View(articles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ViewArticle(GetArticleQuery query)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userId is not null) query.UserId = Guid.Parse(userId.Value);
        var article = await _mediator.Send(query);
        return View(article);
    }

    [HttpGet]
    public async Task<IActionResult> ArticleImage(GetArticleImageQuery query)
    {
        IBlobObject image = await _mediator.Send(query);
        return Ok(image.File);
    }
}
