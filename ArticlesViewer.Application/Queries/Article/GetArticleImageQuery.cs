using ArticlesViewer.Application.RepositoryContracts;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetArticleImageQuery(string Id) : IRequest<IBlobObject>;
