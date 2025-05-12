using CandyPromo.Server.Requests.Validation;
using CandyPromo.Server.Responses;
using Microsoft.Extensions.Options;

namespace CandyPromo.Server.Services;

/// <summary>
/// Сервис для работы с промокодами.
/// </summary>
[Service]
public class PromocodeService(CandyPromoContext database, IOptions<AppSettings> appSettings)
{
    /// <summary>
    /// Метод регистрации промокода за указанным пользователем.
    /// </summary>
    public async Task RegisterByUser(Guid userId, string promocode, CancellationToken cancel)
    {
        var user = await database.Users.SingleOrDefaultAsync(p => p.Id == userId, cancel) ??
            throw new ValidationException("Пользователь не найден.", nameof(userId));

        var code = await database.Promocodes.SingleOrDefaultAsync(p => p.Code == promocode) ??
            throw new ValidationException("Промокод неверный.", nameof(promocode));

        if (code.Owner != null)
            throw new ValidationException("Промокод уже зарегистрирован.", nameof(promocode));

        code.Owner = user;

        await database.SaveChangesAsync(cancel);
    }

    /// <summary>
    /// Возвращает количество промокодов, участвующих в промоакции.
    /// В том числе количество зарегистрированных промокодов.
    /// </summary>
    public async Task<PromocodesCountResponse> GetPromocodesCount(CancellationToken cancel)
    {
        var totalCount = await database.Promocodes.CountAsync(cancellationToken: cancel);
        var registeredCount = await database.Promocodes.CountAsync(p => p.OwnerId != null, cancellationToken: cancel);

        return new PromocodesCountResponse(totalCount, registeredCount);
    }

    /// <summary>
    /// Возвращает список промокодов указанного пользователя.
    /// </summary>
    public async Task<List<MyPromocodeResponse>> GetMyPromocodes(Guid userId, CancellationToken cancel)
    {
        var myPromocodes = await database.Promocodes.Include(p => p.Prize).Where(p => p.OwnerId == userId).ToListAsync(cancel);

        var promoEndDate = appSettings.Value.PromoEndingDate;

        var promoIsDone = DateTime.Now > promoEndDate;

        return myPromocodes.ConvertAll(p => new MyPromocodeResponse(p.Code, p.Prize?.Name, GetStatus(p)));

        string GetStatus(Promocode promocode)
        {
            if (!promoIsDone)
            {
                return "участвует в розыгрыше";
            }

            if (promocode.Prize == null)
            {
                return "Без выигрыша";
            }

            return "Победитель!";
        }
    }
}