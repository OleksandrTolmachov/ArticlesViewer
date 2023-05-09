using ArticlesViewer.Application.RepositoryContracts;

namespace ArticlesViewer.Infrastructure;
public class BlobObject : IBlobObject
{
    public Stream? File { get; set; }
    public string? ContentType { get; set; }
}
