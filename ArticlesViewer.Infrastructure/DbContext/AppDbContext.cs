using ArticlesViewer.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        //string articlesAsJson = File.ReadAllText("seedarticles.json");
        //var articles = JsonSerializer.Deserialize<List<Article>>(articlesAsJson);
        //builder.Entity<Article>().HasData(articles!);
    }

    public virtual DbSet<Article> Articles { get; set; }
}