using System.ComponentModel.DataAnnotations;

namespace Auth.Model.Requests;

public class RefreshTokenRequest
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}