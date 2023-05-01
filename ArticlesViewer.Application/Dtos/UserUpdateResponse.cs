using Microsoft.AspNetCore.Http;

namespace ArticlesViewer.Application.DTO;

public class UserUpdateResponse
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}
