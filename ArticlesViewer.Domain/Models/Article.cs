using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArticlesViewer.Domain;

public class Article
{
    [Key]
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    [ForeignKey(nameof(UserId))]    
    public User? User { get; set; }
    public DateTime PublicationDate { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid TagId { get; set; }
    public TopicTag? TopicTag { get; set; }
}
