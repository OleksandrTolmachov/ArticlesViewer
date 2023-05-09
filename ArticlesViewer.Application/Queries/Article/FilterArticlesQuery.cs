using ArticlesViewer.Application.DTO;
using ArticlesViewer.Domain.Enums;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace ArticlesViewer.Application.Queries;

public record FilterArticlesQuery(TopicTags? Tag, Order Order = Order.Top) 
    : IRequest<IEnumerable<ArticleResponse>>
{
    [Display(Name = "Title must contain")]
    public string TitleContains { get; set; } = "";
}
