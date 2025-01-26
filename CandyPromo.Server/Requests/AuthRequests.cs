using CandyPromo.Server.Requests.Validation;
using System.ComponentModel.DataAnnotations;

namespace CandyPromo.Server.Requests;

/// <summary>
/// Запрос на логин (получение токена).
/// </summary>
public class LoginRequest : IRequest
{
    public string? Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Производит валидацию модели запроса.
    /// </summary>
    public void Validate(ValidationSession session)
    {
        Email = session.ValidateString(Email, maxLength: 50, isRequired: false);
        Password = session.ValidateString(Password, maxLength: 50, isRequired: false);

        Phone = Phone?.Replace("-", "");
        Phone = Phone?.Replace("(", "");
        Phone = Phone?.Replace(")", "");
        Phone = Phone?.Replace("+", "");

        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            session.Error("Укажите почту или телефон", nameof(Email), nameof(Phone));

        if (!string.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email))
            session.Error("Некорректный формат", nameof(Email));

        Password = session.ValidateString(Password, maxLength: 50);
    }
}

/// <summary>
/// Запрос на регистрацию.
/// </summary>
public class RegisterUserRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Производит валидацию модели запроса.
    /// </summary>
    public void Validate(ValidationSession session)
    {
        Name = session.ValidateString(Name, maxLength: 30);
        Email = session.ValidateString(Email, maxLength: 50, isRequired: false);
        if (Email?.Length == 0)
            Email = null;

        Phone = Phone?.Replace("-", "");
        Phone = Phone?.Replace("(", "");
        Phone = Phone?.Replace(")", "");
        Phone = Phone?.Replace("+", "");
        Phone = session.ValidateString(Phone, maxLength: 50, isRequired: false);
        if (Phone?.Length == 0)
            Phone = null;

        if (Email == null && Phone == null)
            session.Error("Укажите почту или телефон", nameof(Email), nameof(Phone));

        if (Email != null && !new EmailAddressAttribute().IsValid(Email))
            session.Error("Некорректный адрес электронной почты", nameof(Email));

        Password = session.ValidateString(Password, maxLength: 50);
    }
}
