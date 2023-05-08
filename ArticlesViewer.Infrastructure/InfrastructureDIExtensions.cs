using ArticlesViewer.Application.RepositoryContracts;
using ArticlesViewer.Application;
using ArticlesViewer.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using ArticlesViewer.Application.Dtos;

namespace ArticlesViewer.Infrastructure;

public static class InfrastructureDIExtensions
{
    public static void AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddTransient<IRepository<Article>, ArticleRepository>();
        services.AddTransient<IRepository<TopicTag>, Repository<TopicTag>>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IBlobRepository, FakeBlobRepository>();
        services.AddTransient<IArticleUserRepository, ArticleUserRepository>();

        SeedData.Initialize(services.BuildServiceProvider());
    }
}

public class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetService<RoleManager<UserRole>>();

        var roles = Enum.GetNames(typeof(Roles));

        foreach (string role in roles)
        {
            if (!roleManager.RoleExistsAsync(role).Result)
                roleManager.CreateAsync(new UserRole() { Name = role }).Wait();
        }
    }
}
