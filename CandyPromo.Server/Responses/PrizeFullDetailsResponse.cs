namespace CandyPromo.Server.Responses;

/// <summary>
/// Ответный пакет с полной информацией о призе.
/// </summary>
/// <param name="Id"> Идентификатор </param>
/// <param name="Name"> Название </param>
/// <param name="ImageUrl"> Ссылка на иконку </param>
/// <param name="Description"> Описание </param>
/// <param name="Status"> Статус </param>
/// <param name="WinnerCode"> Промокод победитель </param>
/// <param name="WinnerName"> ФИО победителя </param>
public record PrizeFullDetailsResponse(
    Guid Id,
    string Name,
    string? ImageUrl,
    string? Description,
    PrizeDeliveryStatus Status,
    string? WinnerCode,
    string? WinnerName);
