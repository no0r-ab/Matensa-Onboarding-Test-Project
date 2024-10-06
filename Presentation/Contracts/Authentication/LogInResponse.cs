namespace Presentation.Contracts.Authentication;

public record LogInResponse(Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);