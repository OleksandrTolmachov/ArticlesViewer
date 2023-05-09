using MediatR;

namespace ArticlesViewer.Application.Commands;

public record DeleteArticleCommand(Guid Id) : IRequest;


