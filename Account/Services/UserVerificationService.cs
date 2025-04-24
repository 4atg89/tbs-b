using Account.Dto;

namespace Account.Services;

public class UserVerificationService(TimeProvider timeProvider) : IUserVerificationService
{




    // private async Task<CodeExpirationDto> GenerateAndSendVerificationCode(string email, Guid userId)
    // {

    //     var code = GenerateFourDigitCode();

    //     var timeToExpire = 300L;
    //     var expiration = timeProvider.GetUtcNow().AddSeconds(timeToExpire).UtcDateTime;
    //     return new CodeExpirationDto { CodeTimeExpirationSeconds = timeToExpire, Id = userId };
    // }

    // private string GenerateFourDigitCode()
    // {
    //     int number = new Random().Next(0, 10000);
    //     return number.ToString("D4");
    // }
    public void VerifyUser(string email, Guid userId)
    {

    }
}