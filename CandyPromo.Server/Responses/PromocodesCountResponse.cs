namespace CandyPromo.Server.Responses;

/// <summary>
/// Содержит информации об общем количестве промокодов и о зарегистрированном количестве.
/// </summary>
/// <param name="TotalCount"> Общее количество. </param>
/// <param name="RegistersCount"> Количество зарегистрированных промокодов </param>
public record PromocodesCountResponse(
    int TotalCount,
    int RegistersCount);