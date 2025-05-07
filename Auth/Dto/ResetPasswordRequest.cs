using System.ComponentModel.DataAnnotations;

namespace Auth.Dto;

public class ResetPasswordRequest
{
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

}
