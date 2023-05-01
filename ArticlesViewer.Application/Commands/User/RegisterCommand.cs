using MediatR;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace ArticlesViewer.Application.Commands;

public record RegisterCommand : IRequest<IdentityResult>
{
    [Required]
    [Display(Name = "Username")]
    [Remote("IsNameInUse", "Account")]
    public string? UserName { get; set; }

    [Required]
    [EmailAddress]
    [Remote("IsEmailInUse", "Account")]
    public string? Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Required]
    [Display(Name = "Confirm password")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password),
        ErrorMessage = "The confirmation password is not repeated correctly.")]
    public string? ConfirmPassword { get; set; }
}
