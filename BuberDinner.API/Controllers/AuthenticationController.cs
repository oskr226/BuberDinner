﻿using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.API.Controllers;

[Route("auth")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest register)
    {
        var authResult = _authenticationService.Register(register.FirstName, register.LasrName, register.Email, register.Password);

        return Ok(authResult);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest login)
    {
        return Ok(login);
    }
}
