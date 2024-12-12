using Microsoft.EntityFrameworkCore;

namespace CandyPromo.Server.HostedServices;

/// <summary>
/// Сервис розыгрыша призов.
/// </summary>
public class PrizeDrawHostedService(ILogger<PrizeDrawHostedService> logger, IServiceProvider services) : IHostedService, IDisposable
{
    /// <summary>
    /// Метод запуска сервиса
    /// </summary>
    public async Task StartAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Сервис розыгрыша призов начал работу и ожидает время розыгрыша.");

        // Дата розыгрыша призов.
        var datePrizeDraw = new DateTime(2025, 1, 1, 22, 00, 0);

        // Ожидание времени розыгрыша.
        await Task.Delay(datePrizeDraw - DateTime.Now, stoppingToken);

        // Получение CandyPromoContext
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

        var registeredPromoCodes = await context.Promocodes
            .Where(p => p.OwnerId != null)
            .Include(x => x.Owner)
            .Include(x => x.Prize)
            .ToListAsync(stoppingToken);
    }

    /// <summary>
    /// Метод остановки сервиса
    /// </summary>
    public Task StopAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Все призы были разыграны.");

        return Task.CompletedTask;
    }
}