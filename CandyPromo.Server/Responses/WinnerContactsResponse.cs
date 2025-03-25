namespace CandyPromo.Server.Responses;

/// <summary>
/// Сведения о контактах победителя.
/// </summary>
/// <param name="PrizeId"> Идентификатор приза </param>
/// <param name="WinnerId"> Идентификатор победителя </param>
/// <param name="Name"> Имя победителя </param>
/// <param name="Phone"> Телефон </param>
/// <param name="Email"> Электронная почта </param>
public record WinnerContactsResponse(Guid PrizeId, Guid WinnerId, string Name, string? Phone, string? Email);