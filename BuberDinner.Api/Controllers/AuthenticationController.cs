using Microsoft.AspNetCore.Mvc;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Application.Services.Authentication;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Queries.Login;
using MapsterMapper;

namespace   BuberDinner.Api.Controllers;


[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var result = await _mediator.Send(command);

        return result.Match(
                    authResult =>Ok(_mapper.Map<AuthenticationResponse>(result.Value)),
                    errors =>Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var loginQuery = _mapper.Map<LoginQuery>(request);
        var result = await _mediator.Send(loginQuery);

            if(result.IsError && result.FirstError == Errors.Authentication.InvalidCredentials)
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