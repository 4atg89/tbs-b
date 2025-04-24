namespace Account.Services;

public interface IUserVerificationService
{
    void VerifyUser(string email, Guid userId);
}