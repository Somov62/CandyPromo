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
        var datePrizeDraw = new DateTime(2024, 12, 16, 21, 59, 0);

        // Ожидание времени розыгрыша.
        await Task.Delay(datePrizeDraw - DateTime.Now, stoppingToken);

        // Получение CandyPromoContext
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

        // Получение всех зарегистрированных промокодов.
        var registeredPromoCodes = await context.Promocodes.AsNoTracking()
                                                           .Where(p => p.OwnerId != null)
                                                           .Include(u => u.Owner)
                                                           .ToListAsync(stoppingToken);

        if (registeredPromoCodes.Count == 0)
        {
            logger.LogInformation("No promocodes.");
            return;
        }

        // Получение всех призов.
        var prizes = await context.Prizes.AsNoTracking()
                                         .ToListAsync(stoppingToken);

        Random random = new();

        // Список выигрышных призов.
        List<Prize> prizesWin = [];

        // Список выигрышных промокодов.
        List<Promocode> promocodesWin = [];

        foreach (var prize in prizes)
        {
            if (registeredPromoCodes.Count == 0)
                break;

            // Получение промокода для приза.
            var promo = registeredPromoCodes[random.Next(0, registeredPromoCodes.Count)];

            logger.LogInformation($"{promo.Owner!.Name} winnner {prize.Name}");

            // Устанвка свойст приза.
            prize.Status = PrizeDeliveryStatus.WinnerFinding;
            prize.PromocodeId = promo.Code;
            prizesWin.Add(prize);

            // Установка свойств промокода.
            promo.PrizeId = prize.Id;
            promo.Owner = null;
            promocodesWin.Add(promo);

            // Удаление выигрышного промокода из списка зарегистрированных.
            registeredPromoCodes.Remove(promo);
        }

        // Сохранение изменений.
        context.UpdateRange(prizesWin);
        context.UpdateRange(promocodesWin);
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