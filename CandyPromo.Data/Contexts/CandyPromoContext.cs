namespace CandyPromo.Data.Contexts;

/// <summary>
/// Контекст базы данных.
/// </summary>
/// <param name="options"></param>
public class CandyPromoContext(DbContextOptions<CandyPromoContext> options) : DbContext(options)
{
    #region Configuration

    /// <summary>
    /// Применение конфигураций к моделям.
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PromocodeConfiguration());
        modelBuilder.ApplyConfiguration(new PrizeConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    #endregion

    #region Tables

    /// <summary>
    /// Пользователи.
    /// </summary>
    public DbSet<User> Users => Set<User>();

    /// <summary>
    /// Призы.
    /// </summary>
    public DbSet<Prize> Prizes => Set<Prize>();

    /// <summary>
    /// Промокоды.
    /// </summary>
    public DbSet<Promocode> Promocodes => Set<Promocode>();

    #endregion
}