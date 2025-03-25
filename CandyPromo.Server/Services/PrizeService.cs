using CandyPromo.Server.Requests.Validation;
using CandyPromo.Server.Responses;

namespace CandyPromo.Server.Services;

/// <summary>
/// Сервис для работы с призами.
/// </summary>
[Service]
public class PrizeService(CandyPromoContext database)
{
    /// <summary>
    /// Возвращает краткую информацию о призах.
    /// </summary>
    public async Task<List<PrizeSummaryResponse>> GetPrizesSummary(CancellationToken cancel)
    {
        var prizes = await database.Prizes.ToListAsync(cancel);

        return prizes.ConvertAll(p =>
                    new PrizeSummaryResponse(p.Id, p.Name, p.ImageUrl, p.Description));
    }

    /// <summary>
    /// Возвращает полную информацию о призах.
    /// Включает в себя информацию о победителе.
    /// </summary>
    public async Task<List<PrizeFullDetailsResponse>> GetPrizesFullDetails(CancellationToken cancel)
    {
        var prizes = await database.Prizes
            .Include(p => p.Promocode)
            .ThenInclude(p => p.Owner)
            .ToListAsync(cancel);



        return prizes.ConvertAll(p =>
                    new PrizeFullDetailsResponse(
                        p.Id,
                        p.Name,
                        p.ImageUrl,
                        p.Description,
                        p.Status,
                        p.Promocode?.Code,
                        p.Promocode?.Owner?.Name));
    }

    /// <summary>
    /// Возвращает полную информацию об указанном призе.
    /// Включает в себя информацию о победителе.
    /// </summary>
    public async Task<PrizeFullDetailsResponse> GetPrizeFullDetails(Guid prizeId, CancellationToken cancel)
    {
        var prize = await database.Prizes
            .Include(p => p.Promocode)
            .ThenInclude(p => p.Owner)
            .SingleOrDefaultAsync(p => p.Id == prizeId, cancellationToken: cancel) ??
              throw new ValidationException("Неверный id приза", nameof(prizeId));

        return new PrizeFullDetailsResponse(
                    prize.Id,
                    prize.Name,
                    prize.ImageUrl,
                    prize.Description,
                    prize.Status,
                    prize.Promocode?.Code,
                    prize.Promocode?.Owner?.Name);
    }

    /// <summary>
    /// Возвращает контактные данные победителя указанного приза.
    /// </summary>
    public async Task<WinnerContactsResponse> GetWinnerContacts(Guid prizeId, CancellationToken cancel)
    {
        var prize = await database.Prizes
            .Include(p => p.Promocode)
            .ThenInclude(p => p.Owner)
            .SingleOrDefaultAsync(p => p.Id == prizeId, cancellationToken: cancel) ??
                throw new ValidationException("Неверный id приза", nameof(prizeId));

        var winner = prize.Promocode?.Owner ??
            throw new ValidationException("Приз с идентификатором не разыгран", nameof(prizeId));

        return new(prize.Id, winner.Id, winner.Name, winner.Phone, winner.Email);
    }

    /// <summary>
    /// Обновляет статус приза.
    /// </summary>
    public async Task UpdatePrizeStatus(Guid prizeId, PrizeDeliveryStatus status, CancellationToken cancel)
    {
        var prize = await database.Prizes
           .SingleOrDefaultAsync(p => p.Id == prizeId, cancellationToken: cancel) ??
               throw new ValidationException("Неверный id приза", nameof(prizeId));

        if (prize.Status >= status)
            throw new ValidationException("Нельзя вернуться на шаг назад при обновлении статуса", nameof(status));

        prize.Status = status;

        await database.SaveChangesAsync(cancel);
    }
}