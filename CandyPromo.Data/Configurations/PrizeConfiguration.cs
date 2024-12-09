namespace CandyPromo.Data.Configurations;

/// <summary>
/// Конфигурация сущности "Приз".
/// </summary>
internal class PrizeConfiguration : IEntityTypeConfiguration<Prize>
{
    /// <summary>
    /// Конфигурирование сущности "Приз".
    /// </summary>
    public void Configure(EntityTypeBuilder<Prize> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.ImageName).HasMaxLength(200);
        builder.Property(x => x.Descripton).HasMaxLength(500);
        builder.HasOne(x => x.Promocode).WithOne(x => x.Prize).HasForeignKey<Prize>(x => x.PromocodeId);
    }
}