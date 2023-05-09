namespace ArticlesViewer.Application.RepositoryContracts
{
    public interface IBlobObject
    {
        string? ContentType { get; set; }
        Stream? File { get; set; }
    }
}