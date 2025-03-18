using CandyPromo.Server.Requests.Validation;

namespace CandyPromo.Server.Services;

/// <summary>
/// Сервис для работы с промокодами.
/// </summary>
[Service]
public class PromocodeService(CandyPromoContext database)
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
}