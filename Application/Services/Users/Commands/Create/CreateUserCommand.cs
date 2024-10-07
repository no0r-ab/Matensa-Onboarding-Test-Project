using Application.Services.Users;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.User.Commands.Create;

public record CreateUserCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    string Password,
    double Balance,
    DateTime DateOfBirth,
    string Role) : IRequest<Result<UserResult>>;