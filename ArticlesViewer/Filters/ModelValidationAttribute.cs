using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArticlesViewer.UI.Filters;

public class ModelValidationAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid && context.Controller is Controller controller)
            context.Result = controller.View(controller.ViewData.Model);

        await next();
    }
}
