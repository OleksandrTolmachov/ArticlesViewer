using System.ComponentModel.DataAnnotations;

namespace ArticlesViewer.Domain
{
    public class Image
    {
        [Key]
        public string? ImageId { get; set; }
        public string? Format { get; set; }

        public override string? ToString()
        {
            return $"{ImageId}.{Format}";
        }
    }
}