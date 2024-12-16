namespace CandyPromo.Data.MigrationService;

/// <summary>
/// ������ �������� ���� ������.
/// </summary>
public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "data-migration-service";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    /// <summary>
    /// ��������� �������� ���� ������ � ��������� �� ��������� �������.
    /// </summary>
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
    private static async Task SeedDataAsync(CandyPromoContext dbContext, CancellationToken cancellationToken)
    {
        // �������� ��������� ���������� ����������.
        var strategy = dbContext.Database.CreateExecutionStrategy();

        // ���������� �������� � ����� ������.
        await strategy.ExecuteAsync(async () =>
        {
            #region ���������� ����������

            IEnumerable<Promocode> promocodes = Enumerable.Empty<Promocode>();

            if (!dbContext.Promocodes.Any())
            {
                // ��������� ����������.
                promocodes = PromocodeGenerator.Generate(200);

                // ���������� ���������� � ���� ������.
                await dbContext.Promocodes.AddRangeAsync(promocodes, cancellationToken);
            }

            #endregion

            #region ���������� �������������

            var users = new List<User>() {
                new()
                {
                    Id = Guid.Parse("efdd8d68-614b-4a66-b7be-24fe007a5775"),
                    Name = "admin1",
                    Email = "admin1@mail.ru",
                    Password = "$2a$11$XZrPxRdUlyGMsFtc3MJl1ON8YRvEDCij9YzhVWLjvAtYdBbMTbJ6a",
                    IsAdmin = true,
                    Phone = "79999999999"
                },
                new()
                {
                    Id = Guid.Parse("01bd4d5b-3b1e-47b0-871e-3d47f23c4b51"),
                    Name = "admin2",
                    Email = "admin2@mail.ru",
                    Password = "$2a$11$ga/ISIwFkZFIf5HTz6NORu9P1AiCXG1m.KD6sA6vYhvXzLR2R4zCO",
                    IsAdmin = true,
                    Phone = "79999999999"
                }
            };

            await dbContext.AddRangeAsync(users, cancellationToken);

            #endregion

            #region ���������� ������

            var prizes = new List<Prize>() {
                    new()
                    {
                        Id = Guid.Parse("5fbe708a-1533-402b-8fdc-83c12c547bb0"),
                        Name = "��������� Xiaomi",
                        Description = "��������� 54",
                        ImageUrl = "https://example.com/image1.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("16281fea-783a-4580-ab84-1be7a8c0e46b"),
                        Name = "������� Huawei",
                        Description = "5G ����������",
                        ImageUrl = "https://example.com/image2.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("f9a8f70d-7e13-429b-b964-333a47c82426"),
                        Name = "���������",
                        Description = "������� ������������� �������� ����",
                        ImageUrl = "https://example.com/image3.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("b3c8dcf0-11d9-4c13-b695-04089649f70f"),
                        Name = "������� Dyson",
                        Description = "������ � �����",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("33a1790a-ec92-4ab1-8f1c-222598b0b412"),
                        Name = "������� Huawei Nova 11",
                        Description = "128 ������ � ���� ������ �� 100 ��",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("117710cc-73ee-488b-ac36-b707b51d9118"),
                        Name = "����� ������� Xiaomi",
                        Description = "���� ��� �� � ����",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("94efc89d-50a1-4721-ad36-e6507b6314ee"),
                        Name = "���� Tefal",
                        Description = "� �������������",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("9cbdcbd8-5a9d-4e44-bb62-21453e81515d"),
                        Name = "������ Haval H3",
                        Description = "����� 25 ������ � ������ ��������",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("c892eab4-01db-447f-ab08-fe7fba76511c"),
                        Name = "�������� � �������",
                        Description = "100 ��������� � ���������� ��� �� ����",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("43239b85-f3a1-4c9b-ac9d-154dd5d942e4"),
                        Name = "������� ��",
                        Description = "RTX 4090 � GTA VI",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                };

            await dbContext.AddRangeAsync(prizes, cancellationToken);

            #endregion

            // ���������� ��������� � ���� ������.
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}