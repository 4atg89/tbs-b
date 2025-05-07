using System.ComponentModel.DataAnnotations;

namespace Auth.Dto;

public class RegistrationRequest
{
    [Required]
    [PasswordValidation]
    public required string Password { get; set; }

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    //todo finish errors
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "")]
    [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "")]
    public required string Nickname { get; set; }
}
