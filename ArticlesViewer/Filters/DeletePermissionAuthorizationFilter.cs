using ArticlesViewer.Application.Commands;
using ArticlesViewer.UI.Policies.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArticlesViewer.UI.Filters;

public class DeletePermissionAuthorizationFilter : IAsyncAuthorizationFilter
{
    private readonly IAuthorizationService _authorizationService;

    public DeletePermissionAuthorizationFilter(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Request.HasFormContentType)
        {
            string id = context.HttpContext.Request.Form["Id"];
            var result = await _authorizationService.AuthorizeAsync
                (context.HttpContext.User, new DeleteArticleCommand(Guid.Parse(id)), new AllowDeleteArticleRequirement());

            if (!result.Succeeded)
                context.Result = new ForbidResult();
        }
    }
}
