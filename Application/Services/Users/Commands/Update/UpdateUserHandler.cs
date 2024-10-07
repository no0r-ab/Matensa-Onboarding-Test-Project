using Application.Common.Interfaces;
using MediatR;
using SharedKernel.Result;
using UserDomain = Domain.Users.User;

namespace Application.Services.Users.Commands.Update;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, Result<UserResult>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserResult>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Result<UserResult>.Failure(Error.Validation("Email already Exist."));

        var user = await _userRepository.Get(command.Id);

        if (user is null)
            return Result<UserResult>.Failure(Error.NotFound("404", $"User with ID {command.Id} not found."));

        var updatedUser = UserDomain.Update(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.Balance, command.DateOfBirth, command.IsAdmin);

        await _userRepository.UpdateAsync(user);

        return Result<UserResult>.Success(new UserResult(
            Id: user.Id,
            FullName: updatedUser.FullName,
            Email: updatedUser.Email,
            PhoneNumber: updatedUser.PhoneNumber,
            Balance: updatedUser.Balance,
            DateOfBirth: updatedUser.DateOfBirth,
            IsAdmin: updatedUser.IsAdmin,
            SentTransactions: user.SentTransactions,
            ReceivedTransactions: user.ReceivedTransactions
            ));
    }
}

