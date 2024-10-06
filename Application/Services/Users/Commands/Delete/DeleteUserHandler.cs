using Application.Common.Interfaces;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Commands.Delete;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Result>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            await _userRepository.DeleteAsync(command.Id);

            return Result.Success();
        }
        catch (Exception ex)
        {
            return Result.Failure(Error.Failure("500", ex.Message));
        }

    }
}