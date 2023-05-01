using MediatR;

namespace ArticlesViewer.Application.Commands.Articles;

public record DeleteArticleCommand(string Id) : IRequest;


