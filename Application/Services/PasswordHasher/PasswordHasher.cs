using Application.Common.Interfaces;


namespace Application.Services.PasswordHasher;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string hashedPassword, string password) => BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}