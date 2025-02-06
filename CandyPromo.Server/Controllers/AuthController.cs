using CandyPromo.Server.Models;
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
    /// <summary>
    /// Опции для настройки cookie, используемых в ответах контроллера.
    /// </summary>
    private readonly CookieOptions _cookieOptions = new()
    {
        Expires = DateTime.UtcNow.AddHours(12)
    };

    [HttpPost("register"), AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancel)
    {
        var response = await service.Register(request, cancel);
        SetCookie(response);
        return Ok(response);
    }

    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancel)
    {
        var response = await service.Login(request, cancel);
        SetCookie(response);
        return Ok(response);
    }

    private void SetCookie(TokenResult tokenResult)
    {
        Response.Cookies.Append("token", tokenResult.Token, _cookieOptions);
        Response.Cookies.Append("isAdmin", tokenResult.IsAdmin.ToString(), _cookieOptions);
        Response.Cookies.Append("expire", tokenResult.Expires.ToString(), _cookieOptions);
    }
}