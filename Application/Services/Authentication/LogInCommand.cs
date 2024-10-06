using MediatR;
using SharedKernel.Result;

namespace Application.Services.Authentication;
public record LogInCommand(string Email, string Password) : IRequest<Result<LogInResult>>;