using Application.Services.Authentication;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Authentication;

namespace Presentation.Controllers;

public class LogInController : ApiControllerBase
{
    private readonly ISender _mediator;

    public LogInController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LogInRequest request)
    {
        var command = request.Adapt<LogInCommand>();

        var response = await _mediator.Send(command);

        return response.Match(
             value => Ok(value),
            errors => Problem(errors));
    }

}
