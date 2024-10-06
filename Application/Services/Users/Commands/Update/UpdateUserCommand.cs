using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Commands.Update;

public record UpdateUserCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    double Balance,
    DateTime DateOfBirth,
    string Password,
    bool IsAdmin) : IRequest<Result<UserResult>>;
