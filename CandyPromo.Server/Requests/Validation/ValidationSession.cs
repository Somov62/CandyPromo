using System.Runtime.CompilerServices;

namespace CandyPromo.Server.Requests.Validation;

/// <summary>
/// Удобная обертка над списком ошибок валидации.
/// </summary>
public class ValidationSession
{
    /// <summary>
    /// Список ошибок в рамках данной сессии.
    /// </summary>
    public List<ValidationError> Errors { get; } = [];

    /// <summary>
    /// Добавляет ошибку в список ошибок данной сессии.
    /// </summary>
    public void Error(string propertyName, string reason) =>
        Errors.Add(new(propertyName, reason));

    /// <summary>
    /// Проверяет строку на пустоту и максимальную длину.
    /// А также обрезает пробелы с концов строки.
    /// </summary>
    /// <param name="content"> Строка. </param>
    /// <param name="fieldName"> Название переменной данной строки (Вычисляется автоматически). </param>
    /// <param name="isRequired"> Условие, что поле не должно быть пустым. </param>
    /// <param name="maxLength"> Условие максимальной длины. </param>
    public string ValidateString(
        string? content,
        // Получает название переменной, в которой передавался текст в аргумент content.
        [CallerArgumentExpression(nameof(content))] string fieldName = "",
        bool isRequired = true,
        int? maxLength = null)
    {
        content = content?.Trim()!;

        if (isRequired && string.IsNullOrEmpty(content))
            Errors.Add(new(fieldName, "Поле не заполнено"));

        if (maxLength is not null && content?.Length > maxLength)
            Errors.Add(new(fieldName, $"Превышена максимальная длина поля ({maxLength})"));

        return content!;
    }
}