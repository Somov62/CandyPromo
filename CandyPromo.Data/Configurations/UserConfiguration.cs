namespace CandyPromo.Data.Configurations;

/// <summary>
/// Конфигурация сущности "Пользователь".
/// </summary>
internal class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <summary>
    /// Конфигурирование сущности "Пользователь".
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(50);
        builder.Property(x => x.Email).HasMaxLength(50);
        builder.Property(x => x.Phone).HasMaxLength(11);
        builder.Property(x => x.Password).HasMaxLength(50);
        builder.HasMany(x => x.Promocodes).WithOne(x => x.Owner).IsRequired(false);
    }
}