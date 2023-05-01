using ArticlesViewer.Domain;
using MediatR;

namespace ArticlesViewer.Application.Queries;

public record GetArticleImageQuery(string Id) : IRequest<BlobObject>;
