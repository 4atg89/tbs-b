namespace Auth.GrpcClient;

public interface IUserGrpcProfileService
{
    
    Task RegisterUserAsync(string userId, string nickname);
}