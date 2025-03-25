using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CandyPromo.Server.Controllers;

/// <summary>
/// Контроллер промоакции.
/// </summary>
public class PromoController : BaseController
{
    /// <summary>
    /// Возвращает дату розыгрыша.
    /// </summary>
    [HttpGet("date")]
    [ProducesResponseType(200, Type=typeof(DateTime))]
    public IActionResult GetEndingDate([FromServices]IOptions<AppSettings> appSettings)
    {
        return Ok(appSettings.Value.PromoEndingDate);
    }
}
