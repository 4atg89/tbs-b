using System.ComponentModel.DataAnnotations;

namespace Account.Dto;

public class PasswordRestoringRequest
{
    
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

}
