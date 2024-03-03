namespace BabyAgeCounter.Server.Services;

public interface IUserService
{
 Task<bool> Authenticate(String token);
}