using CandyPromo.Server.Requests;
using CandyPromo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandyPromo.Server.Controllers;

/// <summary>
/// Контроллер для аккаунта.
/// </summary>
public class AuthController(AuthService service) : BaseController
{
    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancel)
    {
        var response = await service.Register(request, cancel);
        return Ok(response);
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancel)
    {
        var response = await service.Login(request, cancel);
        return Ok(response);
    }
}