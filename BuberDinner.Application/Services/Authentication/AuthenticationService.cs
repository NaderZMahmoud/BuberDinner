using BuberDinner.Application.Common.Interfaces.Authentication;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jwtTokenGenerator;

    public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }


    public Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        //check if user exists

        //create user(generate unique id)

        //create JWT token
        Guid userId = Guid.NewGuid(); // In a real implementation, this would come from the created user record
        var token = _jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        return Task.FromResult(new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token));
    }
    public Task<AuthenticationResult> Login(string email, string password)
    {
        return Task.FromResult(new AuthenticationResult(
            Guid.NewGuid(),
            "John",
            "Doe",
            email,
            "token"));
    }
}