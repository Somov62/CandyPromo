namespace CandyPromo.Server.HostedServices;

/// <summary>
/// Сервис розыгрыша призов.
/// </summary>
public class PrizeDrawHostedService(ILogger<PrizeDrawHostedService> logger,
                                    IServiceProvider services) : IHostedService
{
    /// <summary>
    /// Метод запуска сервиса
    /// </summary>
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Service is starting. Wait Time.");

        // Дата розыгрыша призов.
        var datePrizeDraw = new DateTime(2024, 12, 31, 23, 59, 0);

        //// Ожидание времени розыгрыша.
        await Task.Delay(datePrizeDraw - DateTime.Now, stoppingToken);

        // Получение CandyPromoContext
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

        // Получение всех зарегистрированных промокодов.
        var registeredPromoCodes = await context.Promocodes.Where(p => p.OwnerId != null)
                                                           .Include(u => u.Owner)
                                                           .ToListAsync(stoppingToken);

        if (registeredPromoCodes.Count == 0)
        {
            logger.LogInformation("No promocodes.");
            return;
        }

        // Получение всех призов.
        var prizes = await context.Prizes.ToListAsync(stoppingToken);

        Random random = new();

        foreach (var prize in prizes)
        {
            // Получение промокода для приза.
            var promoCodesWithoutPrize = registeredPromoCodes.AsEnumerable()
                                                             .Where(x => x.PrizeId == null);
            if (!promoCodesWithoutPrize.Any())
                break;
            var promoCodesWithoutPrizeCount = promoCodesWithoutPrize.Count();
            Promocode? promo = null;
            if (promoCodesWithoutPrizeCount > 1)
                promo = promoCodesWithoutPrize.ElementAt(random.Next(1, promoCodesWithoutPrizeCount));
            else
                promo = promoCodesWithoutPrize.First();
            promo.PrizeId = prize.Id;
            prize.Status = PrizeDeliveryStatus.WinnerFinding;
            prize.PromocodeId = promo.Code;
            logger.LogInformation($"{promo.Owner!.Name} winnner {prize.Name}");
        }

        await context.SaveChangesAsync(stoppingToken);
        await StopAsync(stoppingToken);
    }

    /// <summary>
    /// Метод остановки сервиса
    /// </summary>
    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Service is stopping.");

        return Task.CompletedTask;
    }
}