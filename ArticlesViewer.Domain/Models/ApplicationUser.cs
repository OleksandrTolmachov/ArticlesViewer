using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Domain;

public class ApplicationUser : IdentityUser
{
    public string? ImageId { get; set; }
    public virtual ICollection<Article>? Articles { get; set; }
}