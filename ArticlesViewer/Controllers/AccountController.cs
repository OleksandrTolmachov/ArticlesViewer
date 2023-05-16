using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.UI.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArticlesViewer.UI.Controllers;


[Route("{controller}/{action}")]
[AllowAnonymous]
public class AccountController : Controller
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [TypeFilter(typeof(ModelValidationActionFilter))]
    public async Task<IActionResult> Register([FromForm] RegisterCommand model)
    {
        var result = await _mediator.Send(model);
        if (result.Succeeded)
            return RedirectToAction("Index", "ArticleViewer");

        foreach (var error in result.Errors)
            ModelState.AddModelError(string.Empty, error.Description);

        return View(model);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    [TypeFilter(typeof(ModelValidationActionFilter))]
    public async Task<IActionResult> Login(LogInCommand model,
        string? returnUrl)
    {
        var signInResult = await _mediator.Send(model);

        if (signInResult.Succeeded)
        {
            if (!string.IsNullOrEmpty(returnUrl)) return LocalRedirect(returnUrl);
            return RedirectToAction("Index", "ArticleViewer");
        }

        ModelState.AddModelError(string.Empty, "Email or password is incorrect.");
        return View(model);
    }

    [Authorize]
    public async Task<IActionResult> UserProfile(GetUserHistoryQuery query)
    {
        ViewBag.History = await _mediator.Send(query);
        ViewBag.WrittenArticles = await _mediator.Send(new GetUserWrittenArticlesQuery(query.UserId));
        var user = await _mediator.Send(new GetUserSettingsQuery(query.UserId.ToString()));
        return View(new UserResponse()
        {
            Email = user.Email,
            UserName = user.UserName
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await _mediator.Send(new LogoutCommand());
        return RedirectToAction("Index", "ArticleViewer");
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> IsEmailInUse(GetIfEmailInUseQuery model)
    {
        return await _mediator.Send(model) ? Json("The email already exists.") : Json(true);
    }

    [AcceptVerbs("Get", "Post")]
    public async Task<IActionResult> IsNameInUse(GetIfNameInUseQuery model)
    {
        return await _mediator.Send(model) ? Json("The name already exists.") : Json(true);
    }

    [HttpGet]
    public async Task<IActionResult> UserSettings()
    {
        var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return View(await _mediator.Send(new GetUserSettingsQuery(id)));
    }

    [HttpPost]
    public async Task<IActionResult> UserSettings(UpdateUserCommand updateCommand)
    {
        updateCommand.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return View(await _mediator.Send(updateCommand));
    }

    [HttpGet]
    public async Task<IActionResult> UserAvatar()
    {
        var image = await _mediator.Send(new GetUserAvatarQuery(User.FindFirstValue(ClaimTypes.NameIdentifier)));
        return Ok(image.File);
    }
}
