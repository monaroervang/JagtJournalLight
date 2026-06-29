using JagtJournal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JagtJournal.Infrastructure.Data.Configurations;

public class HuntEntryConfiguration : IEntityTypeConfiguration<HuntEntry>
{
    // Konfigurerer hvordan HuntEntry skal mappes til databasen
    public void Configure(EntityTypeBuilder<HuntEntry> builder)
    {
        builder.HasKey(huntEntry => huntEntry.Id);

        builder.Property(huntEntry => huntEntry.UserId)
            .IsRequired();

        builder.Property(huntEntry => huntEntry.HuntingAreaId)
            .IsRequired();

        builder.Property(huntEntry => huntEntry.Date)
            .IsRequired();

        builder.Property(huntEntry => huntEntry.CartridgesUsed)
            .IsRequired();

        // EF Core skal bruge backing field i stedet for at ændre listen direkte udefra
        builder.Navigation(huntEntry => huntEntry.Results)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(huntEntry => huntEntry.Results)
            .WithOne()
            .HasForeignKey(huntResult => huntResult.HuntEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        // EF Core skal bruge backing field i stedet for at ændre listen direkte udefra
        builder.Navigation(huntEntry => huntEntry.Companions)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.HasMany(huntEntry => huntEntry.Companions)
            .WithOne()
            .HasForeignKey(huntCompanion => huntCompanion.HuntEntryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(huntEntry => huntEntry.WeatherSnapshot)
            .WithOne()
            .HasForeignKey<WeatherSnapshot>(weatherSnapshot => weatherSnapshot.HuntEntryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}