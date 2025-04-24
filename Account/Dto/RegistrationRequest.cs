using System.ComponentModel.DataAnnotations;

namespace Account.Dto;

public class RegistrationRequest
{
    [Required]
    [PasswordValidation]
    // [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "")]

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
