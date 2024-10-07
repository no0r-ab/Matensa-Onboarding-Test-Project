using Application.Services.Transactions.Create;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authorization; // Required for [Authorize]
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Transaction;
using Presentation.Contracts.Users;
using Presentation.Controllers;

[Authorize(Roles = "User")] // Only users with the "User" role can access this controller
[ApiController]
[Route("api/[controller]")]
public class TransactionController : ApiControllerBase
{
    private readonly ISender _mediator;
    public TransactionController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionRequest request)
    {
        var command = request.Adapt<CreateTransactionCommand>();

        var response = await _mediator.Send(command);

        return response.Match(
                value => Ok(value),
                errors => Problem(errors));
    }
}
