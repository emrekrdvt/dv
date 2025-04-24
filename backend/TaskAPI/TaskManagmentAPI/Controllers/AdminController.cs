using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.Features.Admin;
using TaskManagment.Application.Features.Admin.Query;
using TaskManagment.Application.Features.User.Command;

namespace TaskAPI.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IMediator _mediator;

    public AdminController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("createUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { error = result.Message });

        return Ok(result);
    }
    
    [HttpPut("updateUser")]
    public async Task<IActionResult> CreateUser([FromBody] UpdateUserCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { error = result.Message });

        return Ok(result);
    }
    
    [HttpPost("createHistory")]
    public async Task<IActionResult> CreateHistory([FromBody] CreateHistoryCommand command)
    {
        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { error = result.Message });
        return Ok(result);
    }
    
    [HttpGet("getProjectHistory")]
    public async Task<IActionResult> GetProjectHistory([FromQuery] GetProjectHistoryQuery query)
    {
        var result = await _mediator.Send(query);

        if (!result.Success)
            return BadRequest(new { error = result.Message });

        return Ok(result);
    }
}