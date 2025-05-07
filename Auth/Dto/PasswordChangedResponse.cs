using System.ComponentModel.DataAnnotations;

namespace Auth.Dto;

public class PasswordChangedResponse
{
    public required bool Success { get; set; }
    public required string Message { get; set; }

}
