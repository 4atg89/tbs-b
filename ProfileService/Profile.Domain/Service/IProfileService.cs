namespace Profile.Domain;

public interface IProfileService
{
    Task<string> GetUserProfile();
}
