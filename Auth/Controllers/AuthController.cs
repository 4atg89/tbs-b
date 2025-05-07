using Auth.Dto;
using Auth.Extensions;
using Auth.Model.Requests;
using Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController(
    IAuthService authService,
    IUserVerificationService userVerificationService
) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await authService.Register(request);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status201Created, result.Data);
    }

    [HttpPost]
    [Route("register/confirm-code")]
    public async Task<IActionResult> ConfirmRegisterEmail([FromBody] ConfirmationCodeRequest dto)
    {
        var result = await userVerificationService.VerifyUser(dto.VerificationId, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await authService.Login(dto);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("login/confirm-code")]
    public async Task<IActionResult> ConfirmLoginEmail([FromBody] ConfirmationCodeRequest dto)
    {
        var result = await userVerificationService.VerifyUser(dto.VerificationId, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest dto)
    {
        var result = await authService.Logout(dto.RefreshToken);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> DispatchTokenIfValid([FromBody] RefreshTokenRequest dto)
    {
        var result = await userVerificationService.RefreshToken(dto.RefreshToken);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/restore")]
    public async Task<IActionResult> RestorePasswordByEmail([FromBody] ResetPasswordRequest dto)
    {
        var result = await authService.RestorePasswordByEmail(dto.Email);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/confirm-code")]
    public async Task<IActionResult> PasswordConfirmEmail([FromBody] ConfirmationCodeRequest dto)
    {
        var result = await userVerificationService.VerifyUserCanChangePassword(dto.VerificationId, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/change")]
    public async Task<IActionResult> ChangePassword([FromBody] NewPasswordRequest dto)
    {
        var result = await authService.SetNewPassword(dto);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

}