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

        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            session.Error(nameof(Email) + nameof(Phone), "Укажите почту или телефон");

        if (!string.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email))
            session.Error(nameof(Email), "Некорректный формат");

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
        Phone = session.ValidateString(Phone, maxLength: 50, isRequired: false);

        if (string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Phone))
            session.Error(nameof(Email) + nameof(Phone), "Укажите почту или телефон");

        if (!string.IsNullOrEmpty(Email) && !new EmailAddressAttribute().IsValid(Email))
            session.Error(nameof(Email), "Некорректный адрес электронной почты");

        Password = session.ValidateString(Password, maxLength: 50);
    }
}
