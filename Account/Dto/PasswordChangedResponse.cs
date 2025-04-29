using System.ComponentModel.DataAnnotations;

namespace Account.Dto;

public class PasswordChangedResponse
{
    public required bool Success { get; set; }
    public required string Message { get; set; }

}
