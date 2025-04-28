using Account.Dto;
using Account.Extensions;
using Account.Model.Requests;
using Account.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Account.Controllers;

[Route("api/v1/account")]
[ApiController]
public class AccountController(
    IAccountService accountService,
    IUserVerificationService userVerificationService
) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await accountService.Register(request);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status201Created, result.Data);
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest dto)
    {
        var result = await accountService.Login(dto);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout([FromBody] LogoutRequest dto)
    {
        var result = await accountService.Logout(dto.RefreshToken);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status204NoContent);
    }

    [HttpPost]
    [Route("refresh")]
    public async Task<IActionResult> DispatchTokenIfValid([FromBody] RefreshTokenRequest dto)
    {
        var result = await userVerificationService.DispatchTokenIfValid(dto.RefreshToken);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("confirm-code")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmationCodeRequest dto)
    {
        var result = await userVerificationService.VerifyUser(dto.Id, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/restore")]
    public async Task<IActionResult> RestorePassword([FromBody] ResetPasswordRequest dto)
    {
        var result = await accountService.RestorePassword(dto);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/confirm-code")]
    public async Task<IActionResult> PasswordConfirmEmail([FromBody] ConfirmationCodeRequest dto)
    {
        //todo wrong method
        var result = await userVerificationService.VerifyUserCanChangePassword(dto.Id, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

    [HttpPost]
    [Route("password/change")]
    public async Task<IActionResult> ConfirmChangePassword([FromBody] NewPasswordRequest dto)
    {
        var result = await accountService.SetNewPassword(dto);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }

}