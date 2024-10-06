using MediatR;
using SharedKernel.Result;

namespace Application.Services.Users.Queries.GetAll;

public record GetAllUsersQuery() : IRequest<Result<IEnumerable<UserResult>>>;