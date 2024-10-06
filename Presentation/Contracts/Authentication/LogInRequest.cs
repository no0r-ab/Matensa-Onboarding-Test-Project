using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Authentication;

public record LogInRequest([EmailAddress] string Email, string Password);