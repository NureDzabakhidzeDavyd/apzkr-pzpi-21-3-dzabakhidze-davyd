using System.Security.Claims;
using Kolosok.Application.Features.Auth.Commands;
using Kolosok.Application.Features.Auth.Queries;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kolosok.Presentation.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] LoginUserCommand loginUserCommand)
    {
        var authenticatedResponse = await _mediator.Send(loginUserCommand);
        return Ok(authenticatedResponse);
    }
    
    [HttpGet("profile")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Profile()
    {
        var userIdString = User.Claims.FirstOrDefault(c => c.Type == "sid")?.Value ?? string.Empty;
        if (!Guid.TryParse(userIdString, out var userId))
        {
            return BadRequest("Invalid userId format");
        }

        if (userId == Guid.Empty)
        {
            return Unauthorized();
        }
        
        var profile = await _mediator.Send(new GetUserProfileQuery(userId));
        return Ok(profile);
    }
    
    [HttpPost]
    [Route("refresh")]
    public async Task<ActionResult> Refresh(UserRefreshTokenCommand userRefreshTokenCommand)
    {
        var authenticatedResponse = await _mediator.Send(userRefreshTokenCommand);
        return Ok(authenticatedResponse);
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterUserCommand registerUserCommand)
    {
        var authenticatedResponse = await _mediator.Send(registerUserCommand);
        return Ok(authenticatedResponse);
    }
}