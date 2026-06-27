using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TA.BLL.DTOs.Tasks;
using TA.BLL.Interfaces;
using TA.DAL.Models;

namespace TA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<TaskResponse>> Create([FromBody] CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var response = await taskService.CreateAsync(userId, request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<TaskResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var task = await taskService.GetByIdAsync(id, userId, role, cancellationToken);

        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var tasks = await taskService.GetAllAsync(userId, role, cancellationToken);

        return Ok(tasks);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<TaskPagedResponse>> GetPaged([FromQuery] TaskFilterModel filter, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        var response = await taskService.GetPagedAsync(userId, role, filter, cancellationToken);

        return Ok(response);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        await taskService.UpdateAsync(id, userId, role, request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var role = User.FindFirstValue(ClaimTypes.Role)!;

        await taskService.DeleteAsync(id, userId, role, cancellationToken);

        return NoContent();
    }
}