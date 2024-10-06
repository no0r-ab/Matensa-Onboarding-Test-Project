using Application.Common.Interfaces;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Queries.GetAll;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, Result<IEnumerable<UserResult>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<IEnumerable<UserResult>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetAll();

        var users = result.Select(user => new UserResult(
             Id: user.Id,
             FullName: user.FullName,
             Email: user.Email,
             PhoneNumber: user.PhoneNumber,
             Balance: user.Balance,
             DateOfBirth: user.DateOfBirth
            ));

        return Result<IEnumerable<UserResult>>.Success(users);
    }
}