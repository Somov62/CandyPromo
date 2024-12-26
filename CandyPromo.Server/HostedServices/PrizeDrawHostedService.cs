namespace CandyPromo.Server.HostedServices;

/// <summary>
/// Сервис розыгрыша призов.
/// </summary>
public class PrizeDrawHostedService(
    ILogger<PrizeDrawHostedService> logger,
    IServiceProvider services) : IHostedService, IDisposable
{
    private Timer? _timer = null;

    /// <summary>
    /// Метод запуска сервиса
    /// </summary>
    public Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Service is starting. Wait Time.");

        // Дата розыгрыша призов.
        var datePrizeDraw = new DateTime(2024, 12, 26, 20, 55, 0);

        _timer = new Timer(DrawPrizes, null, TimeSpan.Zero,
            datePrizeDraw - DateTime.Now);

        return Task.CompletedTask;
    }

    /// <summary>
    /// Метод розыгрыша призов.
    /// </summary>
    private void DrawPrizes(object? state)
    {
        // Получение CandyPromoContext
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

        // Получение всех зарегистрированных промокодов.
        var registeredPromoCodes = context.Promocodes.AsNoTracking()
            .Where(p => p.OwnerId != null)
            .Include(u => u.Owner)
            .ToList();

        if (registeredPromoCodes.Count == 0)
        {
            logger.LogInformation("No promocodes.");
            return;
        }

        // Получение всех призов.
        var prizes = context.Prizes.AsNoTracking()
            .ToList();

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
        context.SaveChanges();
    }

    /// <summary>
    /// Метод остановки сервиса
    /// </summary>
    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}