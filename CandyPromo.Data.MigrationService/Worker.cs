namespace CandyPromo.Data.MigrationService;

/// <summary>
/// Сервис миграции базы данных.
/// </summary>
public class Worker(IServiceProvider serviceProvider,
    IHostApplicationLifetime hostApplicationLifetime) : BackgroundService
{
    public const string ActivitySourceName = "data-migration-service";
    private static readonly ActivitySource s_activitySource = new(ActivitySourceName);

    /// <summary>
    /// Выполняет миграцию базы данных и заполняет ее тестовыми данными.
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        // Создание активности для трассировки.
        using var activity = s_activitySource.StartActivity("Migrating database", ActivityKind.Client);

        try
        {
            // Создание области видимости для сервис-провайдера.
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<CandyPromoContext>();

            // Применение миграций к базе данных.
            await dbContext.Database.EnsureCreatedAsync(cancellationToken);

            // Заполнение базы данных тестовыми данными.
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
    /// Заполняет базу данных тестовыми данными.
    /// </summary>
    private static async Task SeedDataAsync(CandyPromoContext dbContext, CancellationToken cancellationToken)
    {
        // Создание стратегии повторения выполнения.
        var strategy = dbContext.Database.CreateExecutionStrategy();

        // Выполнение операций с базой данных.
        await strategy.ExecuteAsync(async () =>
        {
            #region Заполнение промокодов

            IEnumerable<Promocode> promocodes = Enumerable.Empty<Promocode>();

            if (!dbContext.Promocodes.Any())
            {
                // Генерация промокодов.
                promocodes = PromocodeGenerator.Generate(200);

                // Добавление промокодов в базу данных.
                await dbContext.Promocodes.AddRangeAsync(promocodes, cancellationToken);
            }

            #endregion

            #region Заполнение пользователей

            var users = new List<User>() {
                new()
                {
                    Id = Guid.Parse("efdd8d68-614b-4a66-b7be-24fe007a5775"),
                    Name = "admin1",
                    Email = "admin1@mail.ru",
                    Password = "$2a$11$Q7u85hHw4xUny9TFtfSeeOF1fYbdvEuxc31992aniJ3omCc9DCl6a",
                    IsAdmin = true,
                    Phone = "79999999999"
                },
                new()
                {
                    Id = Guid.Parse("01bd4d5b-3b1e-47b0-871e-3d47f23c4b51"),
                    Name = "admin2",
                    Email = "admin2@mail.ru",
                    Password = "$2a$11$Q7u85hHw4xUny9TFtfSeeOF1fYbdvEuxc31992aniJ3omCc9DCl6a",
                    IsAdmin = true,
                    Phone = "79999999999"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "user1",
                    Email = "user1@mail.ru",
                    Password = "$2a$11$/QAXLgsrRon4rcONXdQO8eT5ArYIQEZ3tePKfmcr4ERgQz1Ej5vaq",
                    IsAdmin = false,
                    Phone = "79999999999"
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    Name = "user2",
                    Email = "user2@mail.ru",
                    Password = "$2a$11$/QAXLgsrRon4rcONXdQO8eT5ArYIQEZ3tePKfmcr4ERgQz1Ej5vaq",
                    IsAdmin = false,
                    Phone = "79999999999"
                }
            };

            await dbContext.AddRangeAsync(users, cancellationToken);

            #endregion

            #region Заполнение призов

            var prizes = new List<Prize>() {
                    new()
                    {
                        Id = Guid.Parse("5fbe708a-1533-402b-8fdc-83c12c547bb0"),
                        Name = "Телевизор Xiaomi",
                        Description = "Диагональ 54",
                        ImageUrl = "https://example.com/image1.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("16281fea-783a-4580-ab84-1be7a8c0e46b"),
                        Name = "Телефон Huawei",
                        Description = "5G технологии",
                        ImageUrl = "https://example.com/image2.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("f9a8f70d-7e13-429b-b964-333a47c82426"),
                        Name = "Кофеварка",
                        Description = "Быстрое приготовление вкусного кофе",
                        ImageUrl = "https://example.com/image3.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("b3c8dcf0-11d9-4c13-b695-04089649f70f"),
                        Name = "Пылесос Dyson",
                        Description = "Мощный и тихий",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("33a1790a-ec92-4ab1-8f1c-222598b0b412"),
                        Name = "Телефон Huawei Nova 11",
                        Description = "128 памяти и мега камера на 100 мп",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("117710cc-73ee-488b-ac36-b707b51d9118"),
                        Name = "Робот пылесос Xiaomi",
                        Description = "Сосёт сам не в себя",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("94efc89d-50a1-4721-ad36-e6507b6314ee"),
                        Name = "Утюг Tefal",
                        Description = "С отпаривателем",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("9cbdcbd8-5a9d-4e44-bb62-21453e81515d"),
                        Name = "Машина Haval H3",
                        Description = "Диски 25 дюймов и климат контроль",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("c892eab4-01db-447f-ab08-fe7fba76511c"),
                        Name = "Квартира в Парусах",
                        Description = "100 квадратов и прекрасный вид на реку",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                    new()
                    {
                        Id = Guid.Parse("43239b85-f3a1-4c9b-ac9d-154dd5d942e4"),
                        Name = "Игровой ПК",
                        Description = "RTX 4090 и GTA VI",
                        ImageUrl = "https://example.com/image4.jpg"
                    },
                };

            await dbContext.AddRangeAsync(prizes, cancellationToken);

            #endregion

            // Сохранение изменений в базе данных.
            await dbContext.SaveChangesAsync(cancellationToken);
        });
    }
}