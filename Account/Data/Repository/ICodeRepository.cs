using Account.Data.Model;

namespace Account.Data.Repository;

public interface ICodeRepository
{
    Task StoreEmail(Guid verificationId, string email, string code, DateTime expiresAt);

    Task<string?> FetchAndRemoveEmail(Guid verificationId, string code);

    Task<int> ValidateAttemptsForFetchingEmail(Guid verificationId, string code);

}