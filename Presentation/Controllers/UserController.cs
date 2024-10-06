using Application.Services.User.Commands.Create;
using Application.Services.Users.Commands.AddBalance;
using Application.Services.Users.Commands.Delete;
using Application.Services.Users.Commands.Update;
using Application.Services.Users.Queries.GetAll;
using Application.Services.Users.Queries.GetById;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Users;

namespace Presentation.Controllers;

[Route("api/users")]
[Authorize(Roles = "Admin")] // Ensure only admin can access this controller
public class UsersController : ApiControllerBase
{
    private readonly ISender _mediator;

    public UsersController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
    {
        var request = new GetUserByIdQuery(id);
        var response = await _mediator.Send(request);

        return response.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var request = new GetAllUsersQuery();
        var response = await _mediator.Send(request);

        return response.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [AllowAnonymous]
    [HttpPost("{role}")]
    public async Task<IActionResult> Create(string role, [FromBody] CreateUserRequest request)
    {
        request.Role = role; // Set the role

        // Use Mapster's Adapt method to map CreateUserRequest to CreateUserCommand
        var command = request.Adapt<CreateUserCommand>();

        var createResult = await _mediator.Send(command);

        return createResult.Match(
            createResult => Created($"api/users/{createResult.Id}", createResult.Adapt<UserResponse>()),
            errors => Problem(errors));
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserRequest request)
    {
        // Map the request to UpdateUserCommand using Mapster
        var command = request.Adapt<UpdateUserCommand>();

        var response = await _mediator.Send(command);
        return response.Match(
            value => Ok(value),
            errors => Problem(errors));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var request = new DeleteUserCommand(id);
        var response = await _mediator.Send(request);

        return response.Match(
            () => Ok(),
            errors => Problem(errors));
    }

    [HttpPut("{id}/balance")]
    public async Task<IActionResult> AddToInitialBalance([FromBody] AddBalanceRequest request)
    {
        // Map the request to AddBalanceCommand using Mapster
        var command = new AddBalanceCommand(request.Id, request.Amount);

        var response = await _mediator.Send(command);

        return response.Match(
            value => Ok(value),
            errors => Problem(errors));
    }
}
