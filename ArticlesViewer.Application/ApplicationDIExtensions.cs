using ArticlesViewer.Application.Commands;
using ArticlesViewer.Application.Handlers;
using Ganss.Xss;
using Microsoft.Extensions.DependencyInjection;

namespace ArticlesViewer.Application;

public static class ApplicationDIExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(CreateArticleCommand).Assembly);
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient<FilterHandler>(_ =>
            (request, articles) =>
            {
                if (!string.IsNullOrWhiteSpace(request.TitleContains))
                    return articles.Where(article => article.Title.Contains(request.TitleContains, StringComparison.OrdinalIgnoreCase));
                return articles;
            }
        );

        services.AddTransient<FilterHandler>(_ =>
            (request, articles) =>
            {
                if (request.Tag is not null)
                    return articles.Where(article => article.TopicTag == request.Tag?.ToString());
                return articles;
            }
        );

        services.AddTransient<FilterHandler>(_ =>
            (request, articles) => request.Order switch
            {
                Order.Recent => articles.OrderByDescending(article => article.PublicationDate),
                Order.Oldest => articles.OrderBy(article => article.PublicationDate),
                Order.Top => articles.OrderByDescending(articles => articles.Views),
                _ => articles.OrderByDescending(articles => articles.Views)
            }
        );

        services.AddSingleton<IHtmlSanitizer>(_ =>
        {
            var sanitizer = new HtmlSanitizer();
            sanitizer.AllowedCssProperties.Clear();
            sanitizer.AllowedAtRules.Clear();
            return sanitizer;
        });
    }
}
