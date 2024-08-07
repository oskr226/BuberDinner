﻿using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;
using MediatR;

namespace BuberDinner.Application.Authentication.Commands.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;

            //1. Validate the user doesn't exist
            if (_userRepository.GetUserByEmail(command.Email) != null)
            {
                //return new AuthenticationResult(false, "User already exist");
                //throw new DuplicateEmailException();
                //return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
                return Errors.User.DuplicateEmail;
            }

            //2. Create user (generate unique ID) & persist to DB
            var user = new User { 
                FirstName = command.FirstName, 
                LastName = command.LastName, 
                Email = command.Email, 
                Password = command.Password 
            };

            _userRepository.Add(user);

            //3. Create Jwt token
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}
