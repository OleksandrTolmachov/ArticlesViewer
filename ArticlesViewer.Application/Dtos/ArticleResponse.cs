using ArticlesViewer.Domain.Enums;

namespace ArticlesViewer.Application.DTO;

public class ArticleResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime PublicationDate { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public long Views { get; set; }
    public string? TopicTag { get; set; }
}
