using Application.Services.Authentication;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Contracts.Authentication;

namespace Presentation.Controllers;

public class LogInController : ApiControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public LogInController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LogInRequest request)
    {
        var query = _mapper.Map<LogInCommand>(request);
        var response = await _mediator.Send(query);

        return response.Match(
             value => Ok(value),
            errors => Problem(errors));
    }

}
