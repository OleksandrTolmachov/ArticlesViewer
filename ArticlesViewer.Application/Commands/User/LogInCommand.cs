using MediatR;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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
