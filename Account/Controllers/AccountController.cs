using Account.Dto;
using Account.Extensions;
using Account.Services;
using Microsoft.AspNetCore.Mvc;

namespace Account.Controllers;

[Route("api/v1/account")]
[ApiController]
public class AccountController(IAccountService accountService
) : ControllerBase
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request)
    {
        var result = await accountService.Register(request);
        return result.Error?.Map(this) ?? StatusCode(StatusCodes.Status201Created, result.Data);
    }
}