using UserDomain = Domain.Users.User;

namespace Application.Services.Authentication;

public record LogInResult(UserDomain User, string Token);