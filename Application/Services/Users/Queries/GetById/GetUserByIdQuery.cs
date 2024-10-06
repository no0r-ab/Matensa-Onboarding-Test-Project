using MediatR;
using SharedKernel.Result;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Users.Queries.GetById;

public record GetUserByIdQuery([Required] Guid Id) : IRequest<Result<UserResult>>;