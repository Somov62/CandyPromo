namespace CandyPromo.Server.Models;

/// <summary>
/// Результат выполнения запроса на получение токена при регистрации или авторизации.
/// </summary>
public class TokenResult
{
    /// <summary>
    /// Токен доступа.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Является ли пользователь администратором.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Время истечения токена.
    /// </summary>
    public DateTime Expires { get; set; } = DateTime.Now.AddHours(12);
}