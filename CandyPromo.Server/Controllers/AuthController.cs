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

    /// <summary>
    /// Регистрация пользователя в системе.
    /// </summary>
    /// <param name="request"> Данные пользователя </param>
    /// <param name="cancel"></param>
    [HttpPost("register"), AllowAnonymous]
    [ProducesResponseType(200, Type = typeof(TokenResult))]
    [ProducesResponseType(400, Type = typeof(Envelope))]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request, CancellationToken cancel)
    {
        var response = await service.Register(request, cancel);
        SetCookie(response);
        return Ok(response);
    }

    /// <summary>
    /// Вход в аккаунт.
    /// </summary>
    /// <param name="request"> Данные пользователя </param>
    /// <param name="cancel"></param>
    [ProducesResponseType(200, Type = typeof(TokenResult))]
    [HttpPost("login"), AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken cancel)
    {
        var response = await service.Login(request, cancel);
        SetCookie(response);
        return Ok(response);
    }

    /// <summary>
    /// Установить cookie в ответе.
    /// </summary>
    /// <param name="tokenResult"></param>
    private void SetCookie(TokenResult tokenResult)
    {
        Response.Cookies.Append("token", tokenResult.Token, _cookieOptions);
        Response.Cookies.Append("isAdmin", tokenResult.IsAdmin.ToString(), _cookieOptions);
        Response.Cookies.Append("expire", tokenResult.Expires.ToString(), _cookieOptions);
    }
}