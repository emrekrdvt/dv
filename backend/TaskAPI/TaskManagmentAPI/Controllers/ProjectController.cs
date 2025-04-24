using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.Features.Project;
using TaskManagment.Application.Features.Project.Commands;
using TaskManagment.Application.Features.User;

namespace TaskAPI.Controllers;

[Route("api/project")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Message });

            return CreatedAtAction(nameof(CreateProject), new { id = result.Data?.ProjectId }, result);
        }
        
        [HttpGet("{projid?}")]
        [Authorize]
        public async Task<IActionResult> GetProject(int? projid =0)
        {
            //userid
            var userid = User.Identities.Select( x => x.Claims.FirstOrDefault(x => x.Type == "userid")).FirstOrDefault();
            Console.WriteLine(userid);
            var query = new ProjectQuery()
            {
                UserId = Convert.ToInt32(userid.Value),
                ProjectId = projid
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] UpdateProjectCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result.Success)
                return BadRequest(new { error = result.Message });

            return Ok(result);
        }
        
        [HttpPost("addusertoproject")]
        public async Task<IActionResult> AddUserToProject([FromBody] ProjectUserAddCommand command)
        {
            var result = await _mediator.Send(command);

            if (!result)
                return BadRequest(new { error = "Hata"});

            return Ok(result);
        }
    
}