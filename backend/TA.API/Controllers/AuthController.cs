using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TA.BLL.DTOs.Identity;
using TA.BLL.Interfaces;

namespace TA.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;

    public AuthController(IUserService userService, IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserResponse>> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateAsync(request, cancellationToken);

        return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
    {
        var user = await _authService.LoginAsync(request, cancellationToken);

        return Ok(user);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<AuthResponse>> Refresh([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _authService.RefreshAsync(refreshToken, cancellationToken);

        return Ok(token);
    }

    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        await _authService.RevokeAsync(refreshToken, cancellationToken);

        return NoContent();
    }
}