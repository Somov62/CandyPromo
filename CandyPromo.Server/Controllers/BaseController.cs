using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CandyPromo.Server.Controllers;

/// <summary>
/// Базовая реализация контроллера,
/// все контроллеры должны наследоваться от этого.
/// </summary>
[Authorize]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    /// <summary>
    /// Возвращает ответ с кодом 200, обёрнутый в конверт.
    /// </summary>
    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        return base.Ok(Envelope.Ok(value));
    }

    /// <summary>
    /// Возвращает ответ с кодом 400, обёрнутый в конверт.
    /// </summary>
    [NonAction]
    public BadRequestObjectResult BadRequest([ActionResultObjectValue] ValidationError error)
    {
        return base.BadRequest(Envelope.Error(error));
    }

    /// <summary>
    /// Достает идентификатор пользователя из токена авторизации.
    /// </summary>
    protected Guid GetUserId()
    {
        string id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
            throw new InvalidDataException("В валидном токене не оказалось userId");

        return new Guid(id);
    }
}