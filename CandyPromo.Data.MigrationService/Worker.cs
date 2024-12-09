using CandyPromo.Data.MigrationService.SeedData;

namespace CandyPromo.Data.MigrationService;

public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "data-migration-service";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

            await dbContext.Database.EnsureCreatedAsync(cancellationToken);
            await dbContext.Database.MigrateAsync(cancellationToken);
            await SeedDataAsync(dbContext, cancellationToken);
        }
        catch (Exception ex)
        {
            activity?.AddException(ex);
            throw;
        }

        hostApplicationLifetime.StopApplication();
    }

    private static async Task SeedDataAsync(CandyPromoContext dbContext, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (!dbContext.Promocodes.Any())
            {
                var promocodes = PromocodeGenerator.Generate(200);
                await dbContext.Promocodes.AddRangeAsync(promocodes, cancellationToken);
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}