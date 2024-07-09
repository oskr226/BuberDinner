//using BuberDinner.Application.Services.Authentication.Commands;
//using BuberDinner.Application.Services.Authentication.Queries;
//using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
using MediatR;
using BuberDinner.Application.Authentication.Commands.Register;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Application.Authentication.Queries.Login;
//using OneOf;

namespace BuberDinner.API.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IMediator _mediator;

    public AuthenticationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    //Ya no se usará la inyección de dependencias directamente. Ahora se realizará a través del _mediator
    //private readonly IAuthenticationCommandService _authenticationCommandService;
    //private readonly IAuthenticationQueryService _authenticationQueryService;

    //public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
    //{
    //    _authenticationCommandService = authenticationCommandService;
    //    _authenticationQueryService = authenticationQueryService;
    //}

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest register)
    {
        var command = new RegisterCommand(register.FirstName, register.LasrName, register.Email, register.Password);

        ErrorOr<AuthenticationResult> registerResult = await _mediator.Send(command);
        //ErrorOr<AuthenticationResult> registerResult = _authenticationCommandService.Register(register.FirstName, register.LasrName, register.Email, register.Password);

        return registerResult.Match(registerResult => Ok(MapAuthenticationResponse(registerResult)),
            errors => Problem(errors)
            );

        //if(registerResult.IsSuccess)
        //{
        //    return Ok(MapAuthResult(registerResult.Value));
        //}

        //var firstError = registerResult.Errors[0];

        //if(firstError is DuplicateEmailError)
        //{
        //    return Problem(statusCode: StatusCodes.Status409Conflict, detail: "Email already exists.");
        //}

        //return Problem();
    }

    private static AuthenticationResponse MapAuthenticationResponse(AuthenticationResult authenticationResult)
    {
        return new AuthenticationResponse(
            authenticationResult.User.Id,
            authenticationResult.User.FirstName,
            authenticationResult.User.LastName,
            authenticationResult.User.Email,
            authenticationResult.Token
            );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.User.Id, authResult.User.FirstName, authResult.User.LastName, authResult.User.Email, authResult.Token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest login)
    {
        var query = new LoginQuery(login.Email, login.Password);

        ErrorOr<AuthenticationResult> authResult = await _mediator.Send(query);

        //ErrorOr<AuthenticationResult> authResult = _authenticationQueryService.Login(login.Email, login.Password);

        if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
        }

        return authResult.Match(authResult => Ok(MapAuthenticationResponse(authResult)),
            errors => Problem(errors)
            );

        //var repsonse = new AuthenticationResponse(authResult.Value.user.Id, authResult.Value.user.FirstName, authResult.Value.user.LastName, authResult.Value.user.Email, authResult.Value.Token);

        //return Ok(repsonse);
    }
}
