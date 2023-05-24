using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ArticlesViewer.UI.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class MaxFileSizeAttribute : Attribute, IAsyncActionFilter
{
    private readonly int _maxSizeInBytes;

    public MaxFileSizeAttribute(int maxSizeInBytes)
    {
        _maxSizeInBytes = maxSizeInBytes;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var file = context.ActionArguments.Values.OfType<UpdateUserCommand>().FirstOrDefault()?.Image;
        if (file is not null && file.Length > _maxSizeInBytes)
        {
            context.Result = new BadRequestResult();
            return;
        }
        
        await next();
    }
}
