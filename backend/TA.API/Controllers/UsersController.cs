using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TA.BLL.DTOs.Identity;
using TA.BLL.Interfaces;
using TA.DAL.Enums;

namespace TA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = nameof(UserRole.Administrator))]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(id, cancellationToken);
        return Ok(user);
    }

    [HttpGet("email")]
    public async Task<ActionResult<UserResponse>> GetByEmail([FromQuery] string email, CancellationToken cancellationToken)
    {
        var user = await userService.GetByEmailAsync(email, cancellationToken);
        return Ok(user);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var users = await userService.GetAllAsync(cancellationToken);
        return Ok(users);
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await userService.UpdateAsync(id, request, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        await userService.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}