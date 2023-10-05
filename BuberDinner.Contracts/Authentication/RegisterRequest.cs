namespace BuberDinner.Contracts.Authentication;

public record RegisterRequest(
    string FirstName,
    string LasrName,
    string Email,
    string Password
);