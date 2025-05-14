namespace Profile.Domain.Repository;

public interface IProfileRepository
{
    Task<string> GetProfile(Guid id);

}