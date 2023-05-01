using System.ComponentModel.DataAnnotations;
using ArticlesViewer.Domain.Enums;

namespace ArticlesViewer.Domain
{
    public class Article
    {
        [Key]
        public Guid Id { get; set; }
        public ApplicationUser? User { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TopicTags TopicTag { get; set; }
    }
}