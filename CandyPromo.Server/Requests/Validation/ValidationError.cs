namespace CandyPromo.Server.Requests.Validation;

/// <summary>
/// Ошибка валидации.
/// </summary>
/// <param name="PropertyName"> Поле в котором ошибка. </param>
/// <param name="Reason"> Причина возникновения. </param>
public record ValidationError(string PropertyName, string Reason);
