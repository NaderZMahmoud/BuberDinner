namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    public Task<AuthenticationResult> Login(string email, string password)
    {
        return Task.FromResult(new AuthenticationResult(
            Guid.NewGuid(),
            "John",
            "Doe",
            email,
            "token"));
    }

    public Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        return Task.FromResult(new AuthenticationResult(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            "token"));
    }
}