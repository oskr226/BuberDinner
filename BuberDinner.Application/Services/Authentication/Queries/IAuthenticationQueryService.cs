﻿using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Services.Authentication.Common;
using ErrorOr;
using FluentResults;
//using OneOf;

namespace BuberDinner.Application.Services.Authentication.Queries;

public interface IAuthenticationQueryService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);
}
