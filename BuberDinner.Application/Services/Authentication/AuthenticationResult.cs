namespace BuberDinner.Application.Services.Authentication;

public record AuthenticationResult(
    Guid Id,
    string FirstName,
    string LasrName,
    string Email,
    string Token
);
