using Application.Common.Interfaces;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Queries.GetById;

public class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, Result<UserResult>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserResult>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetByIdAsync(request.Id);

        return Result<UserResult>.Success(new UserResult(
            Id: result.Id,
            FullName: result.FullName,
            Email: result.Email,
            PhoneNumber: result.PhoneNumber,
            Balance: result.Balance,
            DateOfBirth: result.DateOfBirth
            ));
    }
}