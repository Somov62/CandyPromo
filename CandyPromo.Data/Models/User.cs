namespace CandyPromo.Data.Models;

/// <summary>
/// Пользователь.
/// </summary>
public class User
{
    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public required Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Email пользователя.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Телефон пользователя.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Пароль пользователя.
    /// </summary>
    public required string Password { get; set; }

    /// <summary>
    /// Является ли пользователь администратором.
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// Промокоды пользователя.
    /// </summary>
    public List<Promocode>? Promocodes { get; set; }
}