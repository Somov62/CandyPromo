namespace CandyPromo.Server.Auth;

/// <summary>
/// Класс секции конфигурационного файла,
/// содержащий настройки для jwt токена.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// Ключ шифрования.
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Срок жизни токена в часах.
    /// </summary>
    public int ExpiresHours { get; set; }
}