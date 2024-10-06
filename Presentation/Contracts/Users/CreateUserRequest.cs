using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Presentation.Contracts.Users;

public record CreateUserRequest(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    double Balance,
    DateTime DateOfBirth)
{
    [JsonIgnore] // This will prevent it from showing in Swagger
    public string Role { get; set; } = string.Empty; // Initialize with a default value
}

/*public record CreateUserRequest(
     string FirstName,
     string LastName,
     string Email,
     string PhoneNumber,
     DateTime DateOfBirth,
     string Password);*/