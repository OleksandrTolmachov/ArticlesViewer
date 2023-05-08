using MediatR;

namespace ArticlesViewer.Application.Commands;

public record AddUserArticleHistoryCommand(Guid ArticleId, Guid? UserId) : IRequest;

