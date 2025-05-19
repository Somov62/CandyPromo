namespace CandyPromo.Server.HostedServices;

/// <summary>
/// Сервис розыгрыша призов.
/// </summary>
public class PrizeDrawHostedService(
    IOptions<AppSettings> appSettings,
    IServiceProvider services) : IHostedService, IDisposable
{
    private Timer? _timer = null;
    private readonly DateTime _prizeDate = appSettings.Value.PromoEndingDate;
    private readonly ILogger<PrizeDrawHostedService> _logger = services.GetRequiredService<ILogger<PrizeDrawHostedService>>();

    /// <summary>
    /// Метод запуска сервиса
    /// </summary>
    public Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Service is starting. Wait Time.");

        if (_prizeDate - DateTime.Now < TimeSpan.Zero || (uint)(_prizeDate - DateTime.Now).TotalDays > 45)
        {
            _logger.LogError("Error date");
            return Task.CompletedTask;
        }

        _timer = new Timer(DrawPrizes, null, _prizeDate - DateTime.Now, TimeSpan.FromDays(0));

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
            _logger.LogInformation("No promocodes.");
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

            _logger.LogInformation($"{promo.Owner!.Name} winner {prize.Name}");

            // Установка свойств приза.
            prize.Status = PrizeDeliveryStatus.WinnerFound;
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
        _logger.LogInformation("Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Высвобождает ресурсы.
    /// </summary>
    public void Dispose()
    {
        _timer?.Dispose();
    }
}