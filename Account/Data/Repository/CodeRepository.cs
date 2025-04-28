using StackExchange.Redis;

namespace Account.Data.Repository;

public class CodeRepository(
    TimeProvider timeProvider,
    IConnectionMultiplexer redis
) : ICodeRepository
{

    private readonly IDatabase _database = redis.GetDatabase();

    public async Task StoreEmail(Guid verificationId, string email, string code, DateTime expiresAtt)
    {
        var ttl = expiresAtt - timeProvider.GetUtcNow();
        if (ttl <= TimeSpan.Zero) throw new ArgumentException("ttl can not be less then zero for the code validation");
        await _database.StringSetAsync(VerificationCode(verificationId, code), email, ttl);
    }

    public async Task<string?> FetchAndRemoveEmail(Guid verificationId, string code) =>
        await _database.StringGetDeleteAsync(VerificationCode(verificationId, code));

    public async Task<int> ValidateAttemptsForFetchingEmail(Guid verificationId, string code)
    {
        var key = VerificationAttempts(verificationId);
        var result = (int)await _database.StringIncrementAsync(key);
        if (result == 1) await _database.KeyExpireAsync(key, TimeSpan.FromMinutes(5));
        if (result == 4)
        {
            //todo find code as it is probably wrong and email won't be eliminated 
            await _database.KeyDeleteAsync(VerificationCode(verificationId, code));
        }
        return result;
    }

    private static string VerificationCode(Guid verificationId, string code) =>
           $"code:{verificationId}:{code}";

    private static string VerificationAttempts(Guid verificationId) =>
        $"code:attempts:{verificationId}";


}