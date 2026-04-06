using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Presistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJWTTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJWTTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }


    public Task<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
    {
        //1. Check if user already exists
        if (_userRepository.GetUserByEmailAsync(email).Result != null)
        {
            throw new DuplicateEmailException();
        }
        //2. Create user (generate unique id) and persist to database
        var user = new User
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password // In a real implementation, the password should be hashed
        };
        _userRepository.AddUserAsync(user);

        //3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return Task.FromResult(new AuthenticationResult(
            user,
            token));
    }
    public Task<AuthenticationResult> Login(string email, string password)
    {
        //1. Check if user exists
        if (_userRepository.GetUserByEmailAsync(email).Result is not User user)
        {
            throw new Exception("User with this email does not exist");
        }
        //2. Validate password
        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        //3. Create JWT token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return Task.FromResult(new AuthenticationResult(
            user,
            token));
    }
}