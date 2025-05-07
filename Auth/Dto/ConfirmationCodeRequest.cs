using System.ComponentModel.DataAnnotations;

namespace Auth.Dto;

public class ConfirmationCodeRequest
{

    [Required]
    public required Guid VerificationId { get; set; }

    [Required]
    [StringLength(maximumLength: 4, MinimumLength = 4, ErrorMessage = "")]
    public required string Code { get; set; }
}
