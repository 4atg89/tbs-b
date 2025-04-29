using System.ComponentModel.DataAnnotations;

namespace Account.Dto;

public class NewPasswordRequest
{

    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string ResetToken { get; set; }

    [Required]
    [RegularExpression("^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9]).{8,}$", ErrorMessage = "")]
    public required string Password { get; set; }

}
