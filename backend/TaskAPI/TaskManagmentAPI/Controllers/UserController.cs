using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.Features.User;

namespace TaskAPI.Controllers;


[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{userid}")]
    public async Task<IActionResult> GetUser(int userid = -1)
    {
        UsersQuery usq = new UsersQuery()
        {
            Id = userid
        };
        var response = await _mediator.Send(usq);
        return Ok(response);
    }
    
}