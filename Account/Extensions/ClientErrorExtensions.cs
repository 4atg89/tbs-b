using Microsoft.AspNetCore.Mvc;

namespace Account.Extensions;

public static class ClientErrorExtensions
{

    public static IActionResult? Map(this ClientError error, ControllerBase controller)
    {
        return error.ErrorType switch
        {
            ClientErrorType.BadRequest => controller.BadRequest(error.ErrorMessage),
            ClientErrorType.Unauthorized => controller.Unauthorized(error.ErrorMessage),
            ClientErrorType.Forbidden => controller.Forbid(error.ErrorMessage),
            ClientErrorType.NotFound => controller.NotFound(error.ErrorMessage),
            ClientErrorType.Conflict => controller.Conflict(error.ErrorMessage),
            _ => throw new NotImplementedException(),
        };

    }

}