using System.ComponentModel.DataAnnotations;

namespace Auth.Model.Requests;

public class LogoutRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}