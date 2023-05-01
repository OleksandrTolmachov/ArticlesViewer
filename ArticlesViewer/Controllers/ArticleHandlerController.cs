using ArticlesViewer.Application.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ArticlesViewer.Application.Commands.Articles;
using ArticlesViewer.Application.Queries;
using ArticlesViewer.Application.Commands;

namespace ArticlesR.Controllers
{
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
        public async Task<IActionResult> CreateArticle(CreateArticleCommand createRequest)
        {
            if (!ModelState.IsValid) return View(createRequest);

            createRequest.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(createRequest);

            return RedirectToAction("Index", "ArticleViewer");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteArticle(string id)
        {
            await _mediator.Send(new DeleteArticleCommand(id));
            return RedirectToAction("Index", "ArticleViewer");
        }

        [HttpGet]
        public async Task<IActionResult> EditArticle(string id)
        {
            var article = await _mediator.Send(new GetArticleQuery(id));
            return View(article);
        }
    }
}
