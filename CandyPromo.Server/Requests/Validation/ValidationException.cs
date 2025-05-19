using System.Diagnostics.CodeAnalysis;

namespace CandyPromo.Server.Requests.Validation;

/// <summary>
/// Исключение для создания ответа пользователю с информацией об ошибке.
/// </summary>
[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors", Justification = "<Ожидание>")]
public class ValidationException : Exception
{
    /// <summary>
    /// Создает исключение из объекта ошибки валидации.
    /// </summary>
    public ValidationException(ValidationError error)
    {
        Errors = [error];
    }

    /// <summary>
    /// Создает исключение из нескольких объектов ошибки валидации.
    /// </summary>
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Создает исключение из параметров ошибки валидации.
    /// </summary>
    /// <param name="reason"> Причина возникновения ошибки. </param>
    /// <param name="propertyNames"> Имя свойства, значение которого не прошло валидацию. </param>
    public ValidationException(string reason, params string[] propertyNames)
    {
        Errors = [new(reason, propertyNames)];
    }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public IEnumerable<ValidationError> Errors { get; }
}
