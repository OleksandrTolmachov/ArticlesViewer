using ArticlesViewer.Domain.Enums;

namespace ArticlesViewer.Application.DTO;

public class ArticleResponse
{
    public Guid Id { get; set; }
    public string? Title { get; set; }
    public DateTime PublicationDate { get; set; }
    public string? Description { get; set; }
    public string? Content { get; set; }
    public TopicTags TopicTag { get; set; }
}
