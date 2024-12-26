﻿using System.Diagnostics.CodeAnalysis;

namespace CandyPromo.Server.Requests.Validation;

/// <summary>
/// Исключение для создания ответа пользователю с информацией об ошибке.
/// </summary>
[SuppressMessage("Roslynator", "RCS1194:Implement exception constructors", Justification = "<Ожидание>")]
public class ValidationException : Exception
{
    public ValidationException(ValidationError error)
    {
        Errors = [error];
    }

    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Errors = errors;
    }

    /// <summary>
    /// Ошибки.
    /// </summary>
    public IEnumerable<ValidationError> Errors { get; }
}
