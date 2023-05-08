using ArticlesViewer.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class AppDbContext : IdentityDbContext<User, UserRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ArticleUserHistory>().HasKey("ArticleId", "UserId");
    }

    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<TopicTag> TopicTags { get; set; }
    public virtual DbSet<ArticleUserHistory> ArticleUserHistories { get; set; }
}