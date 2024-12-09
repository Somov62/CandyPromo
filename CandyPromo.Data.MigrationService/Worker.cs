namespace CandyPromo.Data.MigrationService;

/// <summary>
/// —ервис миграции базы данных.
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="hostApplicationLifetime"></param>
public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "data-migration-service";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    /// <summary>
    /// ¬ыполн€ет миграцию базы данных и заполн€ет ее тестовыми данными.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // —оздание активности дл€ трассировки.
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            // —оздание области видимости дл€ сервис-провайдера.
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

            // ѕрименение миграций к базе данных.
            await dbContext.Database.MigrateAsync(cancellationToken);

            // «аполнение базы данных тестовыми данными.
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    /// <summary>
    /// «аполн€ет базу данных тестовыми данными.
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task SeedDataAsync(CandyPromoContext dbContext, CancellationToken cancellationToken)
    {
        // —оздание стратегии повторени€ выполнени€.
        var strategy = dbContext.Database.CreateExecutionStrategy();

        // ¬ыполнение операций с базой данных.
        await strategy.ExecuteAsync(async () =>
        {
            if (!dbContext.Promocodes.Any())
            {
                // √енераци€ промокодов.
                var promocodes = PromocodeGenerator.Generate(200);

                // ƒобавление промокодов в базу данных.
                await dbContext.Promocodes.AddRangeAsync(promocodes, cancellationToken);
            }

            // —охранение изменений в базе данных.
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}