using Application.Common.Interfaces;
using Domain.Users;
using MediatR;
using SharedKernel.Result;

namespace Application.Services.Authentication;

public record LogInHandler : IRequestHandler<LogInCommand, Result<LogInResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _hasher;
    private readonly IJWTGenerator _tokenService;
    private readonly IUserTokenRepository _userTokenRepository;

    public LogInHandler(
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

    public async Task<Result<LogInResult>> Handle(LogInCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        var user = _userRepository.GetUserByEmail(request.Email);

        if (user is null)
        {
            return Result<LogInResult>.Failure(Error.Validation("User Doesn't Exist."));
        }

        if (!_hasher.VerifyPassword(user.Password, request.Password))
        {
            return Result<LogInResult>.Failure(Error.Validation("Invalid Password."));
        }

        var token = _tokenService.GenerateToken(user);

        var existingToken = await _userTokenRepository.GetByUserIdAsync(user.Id);

        if (existingToken is null)
        {
            var userToken = new UserToken
            {
                UserId = user.Id,
                Token = token,
                CreationDate = DateTime.UtcNow,
                ExpireDate = DateTime.UtcNow.AddHours(1)
            };

            await _userTokenRepository.AddAsync(userToken);
        }
        else
        {
            existingToken.Token = token;
            existingToken.CreationDate = DateTime.UtcNow;
            existingToken.ExpireDate = DateTime.UtcNow.AddHours(1);

            await _userTokenRepository.UpdateAsync(existingToken);
        }

        return Result<LogInResult>.Success(new LogInResult(
            User: user,
            Token: token
        ));
    }
}