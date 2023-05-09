namespace ArticlesViewer.Domain
{
    public class BlobObject : IBlobObject
    {
        public Stream? File { get; set; }
        public string? ContentType { get; set; }
    }
}