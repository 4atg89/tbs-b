using System.ComponentModel.DataAnnotations;

namespace Account.Model.Requests;

public class LogoutRequest
{
    [Required]
    public required string RefreshToken { get; set; }
}