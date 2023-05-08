using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Domain;

public class User : IdentityUser<Guid>
{
    public string? ImageId { get; set; }
    public virtual ICollection<Article>? WrittenArticles { get; set; }
}
