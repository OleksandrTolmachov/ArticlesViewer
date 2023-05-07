using Microsoft.AspNetCore.Authorization;

namespace ArticlesViewer.UI.Policies.Requirements;

public record AllowDeleteArticleRequirement : IAuthorizationRequirement
{ }
