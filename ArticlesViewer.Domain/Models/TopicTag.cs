using System.ComponentModel.DataAnnotations;

namespace ArticlesViewer.Domain;

public class TopicTag
{
    [Key]
    public Guid Id { get; set; }
    public string Tag { get; set; } = string.Empty; 
}