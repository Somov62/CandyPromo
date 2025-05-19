namespace CandyPromo.Server.Responses;

/// <summary>
/// Дто промокода, которая сообщает выиграл он или нет и что выиграл.
/// </summary>
/// <param name="Code">Буквенное представление промокода.</param>
/// <param name="PrizeName">Название выигранного приза. Если null, то выигрыша нет.</param>
/// <param name="Status">Статус (не разыгран/без выигрыша/победитель)</param>
public record MyPromocodeResponse(string Code, string? PrizeName, string Status);