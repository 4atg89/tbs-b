using System.ComponentModel.DataAnnotations;

namespace Account.Model.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}