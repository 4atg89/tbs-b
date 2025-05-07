namespace Auth.Dto;

public class AuthenticatedUserResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;

}