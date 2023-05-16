using Microsoft.AspNetCore.Http;

namespace ArticlesViewer.Application.DTO;

public class UserSettingResponse
{
    public string Id { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
}
