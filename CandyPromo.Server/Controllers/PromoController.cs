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
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(DateTime))]
    public IActionResult GetEndingDate([FromServices] IOptions<AppSettings> appSettings)
    {
        return Ok(appSettings.Value.PromoEndingDate);
    }

    /// <summary>
    /// Возвращает состояние розыгрыша. Активный или нет.
    /// </summary>
    [HttpGet("active")]
    [AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(bool))]
    public IActionResult CheckPrizesActive([FromServices] IOptions<AppSettings> appSettings)
    {
        return Ok(appSettings.Value.PromoEndingDate > DateTime.Now);
    }
}
