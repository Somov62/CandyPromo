namespace CandyPromo.Server.Helpers;

/// <summary>
/// Хелпер для хэширования пароля.
/// </summary>
public static class PasswordHasher
{
    /// <summary>
    /// Хеширует пароль.
    /// </summary>
    public static string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    /// <summary>
    /// Сравнивает хеш и пароль.
    /// </summary>
    public static bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}
