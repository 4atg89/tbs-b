using Account.Dto;
using Account.Extensions;
using Account.Services;
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
    [Route("code")]
    public async Task<IActionResult> ConfirmEmail([FromBody] ConfirmationCodeRequest dto)
    {
        var result = await userVerificationService.VerifyUser(dto.Id, dto.Code);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status200OK, result.Data);
    }
}