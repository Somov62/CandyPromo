namespace CandyPromo.Data.Contexts;

public class CandyPromoContext(DbContextOptions<CandyPromoContext> options) : DbContext(options)
{
    #region Configuration

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new PromocodeConfiguration());
        modelBuilder.ApplyConfiguration(new PrizeConfiguration());
        base.OnModelCreating(modelBuilder);
    }

    #endregion

    #region Tables

    public DbSet<User> Users => Set<User>();
    public DbSet<Prize> Prizes => Set<Prize>();
    public DbSet<Promocode> Promocodes => Set<Promocode>();

    #endregion
}