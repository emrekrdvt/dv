using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.Features.Admin;
using TaskManagment.Application.Features.Customer;
using TaskManagment.Application.Features.User;

namespace TaskAPI.Controllers;

[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{customerid}")]
    public async Task<IActionResult> GetUser(int customerid = -1)
    {
        CustomerQuery usq = new CustomerQuery()
        {
            id = customerid
        };
        var response = await _mediator.Send(usq);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
    {
        var response = await _mediator.Send(command);
        if (!response.Success)
            return BadRequest(new { error = response.Message });
        
        return Ok(response);
    }
}