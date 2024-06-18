using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using BuberDinner.Domain.Common.Errors;
//using OneOf;

namespace BuberDinner.API.Controllers;

[Route("auth")]
//[ErrorHandlingFilter]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest register)
    {
        ErrorOr<AuthenticationResult> registerResult = _authenticationService.Register(register.FirstName, register.LasrName, register.Email, register.Password);

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
            authenticationResult.user.Id,
            authenticationResult.user.FirstName,
            authenticationResult.user.LastName,
            authenticationResult.user.Email,
            authenticationResult.Token
            );
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    {
        return new AuthenticationResponse(authResult.user.Id, authResult.user.FirstName, authResult.user.LastName, authResult.user.Email, authResult.Token);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        ErrorOr<AuthenticationResult> authResult = _authenticationService.Login(login.Email, login.Password);

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
