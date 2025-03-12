using CandyPromo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace CandyPromo.Server.Controllers;

/// <summary>
/// Контроллер промокодов.
/// </summary>
public class PromocodeController(PromocodeService service) : BaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] string promocode, CancellationToken cancel)
    {
        var userId = GetUserId();

        await service.RegisterByUser(userId, promocode, cancel);

        return Ok();
    }
}