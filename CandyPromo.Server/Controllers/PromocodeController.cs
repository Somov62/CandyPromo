using CandyPromo.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CandyPromo.Server.Controllers;

/// <summary>
/// Контроллер промокодов.
/// </summary>
public class PromocodeController(PromocodeService service) : BaseController
{
    /// <summary>
    /// Регистрация промокода за пользователем.
    /// </summary>
    /// <param name="promocode">Код с упаковки.</param>
    /// <param name="cancel"></param>
    [HttpPost("register")]
    [Authorize(Roles = "User")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400, Type = typeof(Envelope))]
    [ProducesResponseType(401)]
    [ProducesResponseType(403)]
    public async Task<IActionResult> Register([FromBody] string promocode, CancellationToken cancel)
    {
        var userId = GetUserId();

        await service.RegisterByUser(userId, promocode, cancel);

        return Ok();
    }
}