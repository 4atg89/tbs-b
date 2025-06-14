using System.ComponentModel.DataAnnotations;

namespace Profile.API.Model;

public class ChangeNicknameRequest
{

    //todo finish error message
    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "")]
    [RegularExpression("^[a-zA-Z0-9_.-]+$", ErrorMessage = "")]
    public required string NewNickname { get; set; }
}