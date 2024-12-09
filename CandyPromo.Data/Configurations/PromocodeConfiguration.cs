namespace CandyPromo.Data.Configurations;

internal class PromocodeConfiguration : IEntityTypeConfiguration<Promocode>
{
    public void Configure(EntityTypeBuilder<Promocode> builder)
    {
        builder.HasKey(x => x.Code);
        builder.HasOne(x => x.Owner).WithMany(x => x.Promocodes).HasForeignKey(x => x.OwnerId).IsRequired(false);
        builder.HasOne(x => x.Prize)
               .WithOne(x => x.Promocode)
               .HasForeignKey<Prize>(x => x.PromocodeId)
               .IsRequired(false);
    }
}