using ArticlesViewer.Application.Queries;
using Ganss.Xss;
using MediatR;

namespace ArticlesViewer.Application.Handlers;

public class GetSanitizedHtmlHandler : IRequestHandler<GetSanitizedHtmlQuery, string>
{
    private readonly IHtmlSanitizer _htmlSanitizer;

    public GetSanitizedHtmlHandler(IHtmlSanitizer htmlSanitizer)
    {
        _htmlSanitizer = htmlSanitizer;
    }

    public Task<string> Handle(GetSanitizedHtmlQuery request, CancellationToken cancellationToken)
    {
        string sanitizedHtml = _htmlSanitizer.Sanitize(request.Html);
        return Task.FromResult(sanitizedHtml);
    }
}