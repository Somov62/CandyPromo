namespace CandyPromo.Server.Requests.Validation;

/// <summary>
/// Ошибка валидации.
/// </summary>
/// <param name="PropertyNames"> Поле в котором ошибка. </param>
/// <param name="Reason"> Причина возникновения. </param>
public record ValidationError(string Reason, params string[] PropertyNames);
