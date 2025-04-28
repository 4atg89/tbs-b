using StackExchange.Redis;

namespace Account.Data.Repository;

public class CodeRepository(
    TimeProvider timeProvider,
    IConnectionMultiplexer redis
) : ICodeRepository
{

    private readonly IDatabase _database = redis.GetDatabase();
    private const string CODE = "code";
    private const string EMAIL = "email";

    private static string VerificationCode(Guid verificationId) => $"code:{verificationId}";
    private static string VerificationCodeAttempts(Guid verificationId) => $"code:attempts:{verificationId}";
    private static string VerificationPass(Guid verificationId) => $"password:{verificationId}";
    private static string VerificationPassAttempts(Guid verificationId) => $"password:attempts:{verificationId}";

    public async Task StoreEmailAndCode(Guid verificationId, string email, string code, DateTime expiresAt)
    {
        var key = VerificationCode(verificationId);
        var ttl = expiresAt - timeProvider.GetUtcNow();
        if (ttl <= TimeSpan.Zero) throw new ArgumentException("ttl can not be less then zero for the code validation");
        await _database.HashSetAsync(key, [new HashEntry(EMAIL, email), new HashEntry(CODE, code)]);
        await _database.KeyExpireAsync(key, ttl);
    }

    public async Task<string?> FetchAndRemoveEmailAndCode(Guid verificationId, string code)
    {
        var key = VerificationCode(verificationId);
        var entries = await _database.HashGetAllAsync(key);
        if (entries.Length == 0 || entries.FirstOrDefault(e => e.Name == CODE).Value != code) return null;
        await _database.KeyDeleteAsync(key);
        return entries.FirstOrDefault(e => e.Name == EMAIL).Value;
    }

    public async Task ValidateAttemptsForFetchingEmailAndCode(Guid verificationId, string code)
    {
        var key = VerificationCodeAttempts(verificationId);
        var result = await _database.StringIncrementAsync(key);
        if (result == 1) await _database.KeyExpireAsync(key, TimeSpan.FromMinutes(5));
        if (result == 3) await _database.KeyDeleteAsync(VerificationCode(verificationId));
    }

    public async Task StorePasswordValidation(Guid verificationId, string email, DateTime expiresAt)
    {
        var ttl = expiresAt - timeProvider.GetUtcNow();
        if (ttl <= TimeSpan.Zero) throw new ArgumentException("ttl can not be less then zero for the code validation");
        await _database.StringSetAsync(VerificationPass(verificationId), email, ttl);
    }

    public async Task<string?> FetchAndRemovePasswordValidation(Guid verificationId) =>
        await _database.StringGetDeleteAsync(VerificationPass(verificationId));

    public async Task ValidateAttemptsForFetchingPasswordValidation(Guid verificationId)
    {
        var key = VerificationPassAttempts(verificationId);
        var result = await _database.StringIncrementAsync(key);
        if (result == 1) await _database.KeyExpireAsync(key, TimeSpan.FromMinutes(5));
        if (result == 4) await _database.KeyDeleteAsync(VerificationCode(verificationId));
    }
}