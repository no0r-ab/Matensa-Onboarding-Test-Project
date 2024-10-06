using Application.Common.Interfaces;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Commands.AddBalance;

public class AddBalanceHandler : IRequestHandler<AddBalanceCommand, Result<UserResult>>
{
    private readonly IUserRepository _userRepository;
    public AddBalanceHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResult>> Handle(AddBalanceCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Get(command.UserId);

        if (user is null)
            return Result<UserResult>.Failure(Error.NotFound("404", $"User with ID {command.UserId} not found."));

        user.AddAmount(command.Amount);

        await _userRepository.UpdateAsync(user);

        return Result<UserResult>.Success(new UserResult(
            Id: user.Id,
            FullName: user.FullName,
            Email: user.Email,
            PhoneNumber: user.PhoneNumber,
            Balance: user.Balance,
            DateOfBirth: user.DateOfBirth,
            IsAdmin: user.IsAdmin
            ));
    }
}