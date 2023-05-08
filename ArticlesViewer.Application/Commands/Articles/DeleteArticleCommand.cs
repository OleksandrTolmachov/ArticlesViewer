using MediatR;

namespace ArticlesViewer.Application.Commands.Articles;

public record DeleteArticleCommand(Guid Id) : IRequest;


