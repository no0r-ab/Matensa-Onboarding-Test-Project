using Application.Common.Interfaces;
using Application.Services.Users;
using Domain.Users;
using MediatR;
using SharedKernel.Result;
using DomainUser = Domain.Users.User;

namespace Application.Services.User.Commands.Create;

public class CreateUserHandler :
    IRequestHandler<CreateUserCommand, Result<UserResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IJWTGenerator _tokenService;
    private readonly IUserTokenRepository _userTokenRepository;

    public CreateUserHandler(
        IUserRepository userRepository,
        IPasswordHasher hasher,
        IJWTGenerator tokenService,
        IUserTokenRepository userTokenRepository)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _tokenService = tokenService;
        _userTokenRepository = userTokenRepository;
    }

    public async Task<Result<UserResult>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (_userRepository.GetUserByEmail(command.Email) is not null)
            return Result<UserResult>.Failure(Error.Validation("Email is already in use."));

        var user = DomainUser.Create(
            command.FirstName,
            command.LastName,
            command.Email,
            command.PhoneNumber,
            command.Balance,
            command.DateOfBirth,
            command.Role.Equals("Admin"));

        var hashedPassword = _hasher.HashPassword(command.Password);
        user.SetPassword(hashedPassword);

        await _userRepository.Add(user);

        var token = _tokenService.GenerateToken(user);

        var userToken = new UserToken
        {
            UserId = user.Id,
            Token = token,
            CreationDate = DateTime.UtcNow,
            ExpireDate = DateTime.UtcNow.AddMinutes(30)
        };

        await _userTokenRepository.AddAsync(userToken);

        return Result<UserResult>.Success(new UserResult(
           Id: user.Id,
           FullName: user.FullName,
           Email: user.Email,
           PhoneNumber: user.PhoneNumber,
           Balance: user.Balance,
           DateOfBirth: user.DateOfBirth,
           IsAdmin: user.IsAdmin,
           SentTransactions: user.SentTransactions,
           ReceivedTransactions: user.ReceivedTransactions
        ));
    }
}