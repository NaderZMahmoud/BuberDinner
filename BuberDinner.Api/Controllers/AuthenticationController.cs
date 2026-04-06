using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;

namespace   BuberDinner.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var result = await _authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return result.Match(
                    authResult =>Ok(NewMethod(result.Value)),
                    errors =>Problem(errors));
    }

    private IActionResult NewMethod(AuthenticationResult result)
    {
        var response = new AuthenticationResponse(
                result.User.Id,
                result.User.FirstName,
                result.User.LastName,
                result.User.Email,
                result.Token);
        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authenticationService.Login(
            request.Email,
            request.Password);

            if(result.IsError && result.FirstError == Errors.Authintication.InvalidCredentials)
            {
                var errors = result.Errors;
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: errors[0].Description);
            }

        return result.Match(
                    authResult => Ok(NewMethod1(result)),
                    errors => Problem(errors));
    }

    private static AuthenticationResponse NewMethod1(ErrorOr<AuthenticationResult> result)
    {
        return new AuthenticationResponse(
                result.Value.User.Id,
                result.Value.User.FirstName,
                result.Value.User.LastName,
                result.Value.User.Email,
                result.Value.Token);
    }
}