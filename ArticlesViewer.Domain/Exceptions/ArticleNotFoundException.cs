namespace ArticlesViewer.Domain.Exceptions;

public class ArticleNotFoundException : Exception
{
    public ArticleNotFoundException()
    {
    }

    public ArticleNotFoundException(string? message) : base(message)
    {
    }

    public ArticleNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
