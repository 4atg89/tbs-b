using System.ComponentModel.DataAnnotations;

namespace Account.Dto;

public class ResetPasswordRequest
{
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

}
