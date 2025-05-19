namespace CandyPromo.Server.Responses;

/// <summary>
/// Сведения о призе.
/// </summary>
/// <param name="Id"> Идентификатор </param>
/// <param name="Name"> Название </param>
/// <param name="ImageUrl"> Путь к иконке </param>
/// <param name="Description"> Описание </param>
public record PrizeSummaryResponse(Guid Id, string Name, string? ImageUrl, string? Description);
