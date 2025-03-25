using CandyPromo.Server.Responses;
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

    /// <summary>
    /// Возвращает количество промокодов, участвующих в промоакции.
    /// В том числе количество зарегистрированных промокодов.
    /// </summary>
    [HttpGet("count")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(200, Type = typeof(PromocodesCountResponse))]
    [ProducesResponseType(403)]
    public async Task<IActionResult> GetPromocodesCount(CancellationToken cancel)
    {
        var count = await service.GetPromocodesCount(cancel);

        return Ok(count);
    }
}