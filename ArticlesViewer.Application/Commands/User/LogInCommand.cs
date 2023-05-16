using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ArticlesViewer.Application.Commands;

public record LogInCommand : IRequest<SignInResult>
{
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}
