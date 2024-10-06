using Domain.Users;

namespace Application.Common.Interfaces;

public interface IJWTGenerator
{
    string GenerateToken(User user, string role = "User");
}