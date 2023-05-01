
using ArticlesViewer.Application.DTO;
using ArticlesViewer.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ArticlesViewer.Application.Commands;

public record CreateArticleCommand : IRequest
{
    public string UserId { get; set; } = string.Empty;
    [Required]
    [MinLength(3)]
    [MaxLength(28)]
    public string Title { get; init; } = string.Empty;

    [Required]
    [MaxLength(52)]
    public string Description { get; init; } = string.Empty;

    [Required]
    [MinLength(10)]
    public string Content { get; init; } = string.Empty;

    [Required]
    public IFormFile? Image { get; init; }

    [Required]
    [DisplayName("Tag")]
    public TopicTags TopicTag { get; init; }
}


