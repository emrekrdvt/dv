using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagment.Application.Features.Mission.Command;
using TaskManagment.Application.Features.Mission.Query;

namespace TaskAPI.Controllers;

[Route("api/mission")]
public class MissionController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public MissionController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateMission([FromBody] CreateMissionCommand command)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userid");
        if (userIdClaim == null)
            return Unauthorized("Token geçersiz: userid bulunamadı.");

        int userId = Convert.ToInt32(userIdClaim.Value);
        command.UserId = userId;

        var result = await _mediator.Send(command);

        if (!result.Success)
            return BadRequest(new { error = result.Message });

        return CreatedAtAction(nameof(CreateMission), new { id = result.Data?.ProjectId }, result);
    }

    
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateMission([FromBody] UpdateMissionCommand command)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userid");
        if (userIdClaim == null)
            return Unauthorized("Token geçersiz: userid bulunamadı.");

        int userId = Convert.ToInt32(userIdClaim.Value);
        command.UserId = userId;

        var response = await _mediator.Send(command);
        if (!response.Success)
            return BadRequest(new { error = response.Message });

        return Ok(response);
    }

    [HttpDelete("{MissionId}")]
    [Authorize]
    public async Task<IActionResult> DeleteMission(int MissionId)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userid");
        if (userIdClaim == null)
            return Unauthorized("Token geçersiz: userid bulunamadı.");

        int userId = Convert.ToInt32(userIdClaim.Value);

        var command = new DeleteMissionCommand
        {
            MissionId = MissionId,
            UserId = userId
        };

        var result = await _mediator.Send(command);

        if (!result.Success)
            return NotFound(new { error = result.Message });

        return Ok(result);
    }
    
    [HttpGet("{pid}")]
    [Authorize]
    public async Task<IActionResult> GetMission(int pid)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userid");
        if (userIdClaim == null)
            return Unauthorized("Token geçersiz: userid bulunamadı.");

        int userId = Convert.ToInt32(userIdClaim.Value);
        Console.WriteLine(userId);
        var query = new GetMissionQuery()
        {
            pid = pid,
            userid = userId
        };

        var result = await _mediator.Send(query);

        if (!result.Success)
            return NotFound(new { error = result.Message });

        return Ok(result);
    }
    
    
    [HttpGet("myMissions")]
    [Authorize]
    public async Task<IActionResult> GetMyMission(bool? today = false)
    {
        var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == "userid");
        if (userIdClaim == null)
            return Unauthorized("Token geçersiz: userid bulunamadı.");

        int userId = Convert.ToInt32(userIdClaim.Value);
        var query = new GetMyMissionQuery()
        {
            UserId = userId,
            today = today ?? false
        };

        var result = await _mediator.Send(query);

        if (!result.Success)
            return NotFound(new { error = result.Message });

        return Ok(result);
    }
    

    
}