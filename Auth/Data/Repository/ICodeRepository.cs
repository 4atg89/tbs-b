namespace Auth.Data.Repository;

public interface ICodeRepository
{
    Task StoreEmailAndCode(Guid verificationId, string email, string code, DateTime expiresAt);

    Task<string?> FetchAndRemoveEmailAndCode(Guid verificationId, string code);

    Task ValidateAttemptsForFetchingEmailAndCode(Guid verificationId, string code);

    Task StorePasswordValidation(Guid verificationId, string email, DateTime expiresAt);

    Task<string?> FetchAndRemovePasswordValidation(Guid verificationId);

    Task ValidateAttemptsForFetchingPasswordValidation(Guid verificationId);

}