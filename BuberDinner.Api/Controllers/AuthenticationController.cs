using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;

namespace   BuberDinner.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;

    public AuthenticationController(ISender mediator)
    {
        _mediator = mediator;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);
        var result = await _mediator.Send(command);

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
        var loginQuery = new LoginQuery(
            request.Email,
            request.Password);
        var result = await _mediator.Send(loginQuery);

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