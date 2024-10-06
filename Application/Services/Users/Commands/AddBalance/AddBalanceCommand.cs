using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Commands.AddBalance;

public record AddBalanceCommand(Guid UserId, double Amount) : IRequest<Result<UserResult>>;