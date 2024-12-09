namespace CandyPromo.Data.MigrationService;

/// <summary>
/// ������ �������� ���� ������.
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="hostApplicationLifetime"></param>
public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "data-migration-service";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    /// <summary>
    /// ��������� �������� ���� ������ � ��������� �� ��������� �������.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // �������� ���������� ��� �����������.
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            // �������� ������� ��������� ��� ������-����������.
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

            // ���������� �������� � ���� ������.
            await dbContext.Database.MigrateAsync(cancellationToken);

            // ���������� ���� ������ ��������� �������.
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
    /// ��������� ���� ������ ��������� �������.
    /// </summary>
    /// <param name="dbContext"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private static async Task SeedDataAsync(CandyPromoContext dbContext, CancellationToken cancellationToken)
    {
        // �������� ��������� ���������� ����������.
        var strategy = dbContext.Database.CreateExecutionStrategy();

        // ���������� �������� � ����� ������.
        await strategy.ExecuteAsync(async () =>
        {
            if (!dbContext.Promocodes.Any())
            {
                // ��������� ����������.
                var promocodes = PromocodeGenerator.Generate(200);

                // ���������� ���������� � ���� ������.
                await dbContext.Promocodes.AddRangeAsync(promocodes, cancellationToken);
            }

            // ���������� ��������� � ���� ������.
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}