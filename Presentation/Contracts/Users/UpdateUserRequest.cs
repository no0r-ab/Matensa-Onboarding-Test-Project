using System.ComponentModel.DataAnnotations;

namespace Presentation.Contracts.Users;

public record UpdateUserRequest(
    [Required] Guid Id,
    [Required] string FirstName,
    [Required] string LastName,
    [Required][EmailAddress] string Email,
    [Required][Phone] string PhoneNumber,
    [Required] DateTime DateOfBirth,
    [Required] string Password);
