using MediatR;
using SharedKernel.Result;
using System.ComponentModel.DataAnnotations;

namespace Application.Services.Users.Commands.Delete;

public record DeleteUserCommand([Required] Guid Id) : IRequest<Result>;
